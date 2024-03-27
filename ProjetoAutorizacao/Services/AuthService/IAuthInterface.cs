using ProjetoAutorizacao.Dtos;
using ProjetoAutorizacao.Models;

namespace ProjetoAutorizacao.Services.AuthService
{
    public interface IAuthInterface
    {
        Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegisto);
        Task<Response<string>> Login(UsuarioLoginDto usuarioLogin);
    }
}
