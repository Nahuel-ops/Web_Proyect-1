using Contrato;

namespace WebApplication_Ejemplo_1.Interfaces
{
    public interface IAuthInterface
    {
        void Registrar(LoginDto dto);
        LoginRespuestaDTO? Login(LoginDto dto);
    }
}
