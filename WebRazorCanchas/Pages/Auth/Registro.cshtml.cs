using System.Net.Http.Json;
using Contrato;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebRazorCanchas.Pages.Auth
{
    public class RegistroModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegistroModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LoginDto NuevoUsuario { get; set; } = new();

        public string? MensajeError { get; set; }

        [TempData]
        public string? MensajeExito { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(NuevoUsuario.NombreUsuario) || string.IsNullOrWhiteSpace(NuevoUsuario.Password))
            {
                MensajeError = "Por favor, completa todos los campos.";
                return Page();
            }

            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.PostAsJsonAsync("api/Auth/registrar", NuevoUsuario);

            if (!response.IsSuccessStatusCode)
            {
                MensajeError = "No se pudo registrar el usuario (es posible que el nombre ya exista).";
                return Page();
            }

            MensajeExito = "¡Registro exitoso! Ya puedes iniciar sesión con tus credenciales.";
            return RedirectToPage("/Auth/Login");
        }
    }
}
