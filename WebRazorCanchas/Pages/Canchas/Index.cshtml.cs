using System.Net.Http.Headers;
using System.Text.Json;
using Contrato;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebRazorCanchas.Pages.Canchas
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public CanchaDTOs NuevaCancha { get; set; } = new CanchaDTOs();

        [BindProperty]
        public IFormFile? ArchivoImagen { get; set; }

        public List<CanchaItem> ListaCanchas { get; set; } = new List<CanchaItem>();

        [TempData]
        public string? Mensaje { get; set; }

        // Propiedades para Paginación
        public int PaginaActual { get; set; } = 1;
        public int TotalPaginas { get; set; } = 1;
        public int TotalRegistros { get; set; } = 0;
        private const int RegistrosPorPagina = 5;

        public async Task OnGetAsync(int pagina = 1)
        {
            await CargarCanchas(pagina);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarCanchas(1);
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("Api");

                if (ArchivoImagen != null && ArchivoImagen.Length > 0)
                {
                    using var content = new MultipartFormDataContent();
                    using var stream = ArchivoImagen.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(ArchivoImagen.ContentType);
                    content.Add(fileContent, "archivo", ArchivoImagen.FileName);

                    var respImagen = await client.PostAsync("api/Cancha/subir-imagen", content);
                    if (respImagen.IsSuccessStatusCode)
                    {
                        var json = await respImagen.Content.ReadAsStringAsync();
                        using var doc = JsonDocument.Parse(json);
                        NuevaCancha.Imagen = doc.RootElement.GetProperty("url").GetString();
                    }
                    else
                    {
                        Mensaje = "Error al subir la imagen al servidor.";
                        return RedirectToPage();
                    }
                }

                var respuesta = await client.PostAsJsonAsync("api/Cancha", NuevaCancha);

                if (respuesta.IsSuccessStatusCode)
                {
                    Mensaje = "Cancha registrada correctamente con su imagen.";
                }
                else
                {
                    Mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al registrar: {ex.Message}";
            }

            return RedirectToPage();
        }

        private async Task CargarCanchas(int pagina)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Api");
                var resultado = await client.GetFromJsonAsync<List<CanchaItem>>("api/Cancha");
                if (resultado != null)
                {
                    TotalRegistros = resultado.Count;
                    TotalPaginas = (int)Math.Ceiling(TotalRegistros / (double)RegistrosPorPagina);
                    if (TotalPaginas < 1) TotalPaginas = 1;

                    PaginaActual = pagina < 1 ? 1 : (pagina > TotalPaginas ? TotalPaginas : pagina);

                    ListaCanchas = resultado
                        .Skip((PaginaActual - 1) * RegistrosPorPagina)
                        .Take(RegistrosPorPagina)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al cargar canchas: {ex.Message}";
            }
        }

        public class CanchaItem
        {
            public Guid Id { get; set; }
            public string Descripcion { get; set; } = string.Empty;
            public string Direccion { get; set; } = string.Empty;
            public string? Imagen { get; set; }
        }
    }
}
