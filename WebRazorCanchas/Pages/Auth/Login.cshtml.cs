using System.Net.Http.Json;
using Contrato;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebRazorCanchas.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LoginDto Credenciales { get; set; } = new();

        public string? MensajeError { get; set; }

        [TempData]
        public string? MensajeExito { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Credenciales.NombreUsuario) || string.IsNullOrWhiteSpace(Credenciales.Password))
            {
                MensajeError = "Por favor, completa todos los campos.";
                return Page();
            }

            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.PostAsJsonAsync("api/Auth/login", Credenciales);

            if (!response.IsSuccessStatusCode)
            {
                MensajeError = "Usuario o contraseña incorrectos.";
                return Page();
            }

            var resultado = await response.Content.ReadFromJsonAsync<LoginRespuestaDTO>();

            if (resultado != null && !string.IsNullOrEmpty(resultado.Token))
            {
                HttpContext.Session.SetString("JwtToken", resultado.Token);
                HttpContext.Session.SetString("UsuarioNombre", resultado.NombreUsuario);
                HttpContext.Session.SetString("UsuarioRol", resultado.Rol);

                return RedirectToPage("/Canchas/Index");
            }

            MensajeError = "No se pudo recibir el token desde el servidor.";
            return Page();
        }
    }
}
