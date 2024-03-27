using Microsoft.EntityFrameworkCore;
using ProjetoAutorizacao.Data;
using ProjetoAutorizacao.Dtos;
using ProjetoAutorizacao.Models;
using ProjetoAutorizacao.Services.SenhaService;

namespace ProjetoAutorizacao.Services.AuthService
{
    public class AuthService : IAuthInterface
    {
        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;
        public AuthService(AppDbContext context, ISenhaInterface senhaInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
        }

        public async Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegisto)
        {
            Response<UsuarioCriacaoDto> respostaServico = new Response<UsuarioCriacaoDto>();
            try
            {
                if(!VerificaSeEmailEUsuarioJaExistem(usuarioRegisto))
                {
                    respostaServico.Dados = null;
                    respostaServico.Status = false;
                    respostaServico.Mensagem = "E-mail ou usuário já cadastrados";

                    return respostaServico;
                }
                _senhaInterface.CriarSenhaHash(usuarioRegisto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = new UsuarioModel()
                {
                    Usuario = usuarioRegisto.Usuario,
                    Email = usuarioRegisto.Email,
                    Cargo = usuarioRegisto.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                respostaServico.Mensagem = "Usuário cadastrado com sucesso";


            }
            catch (Exception ex) 
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = ex.Message;
                respostaServico.Status = false;
            }
            return respostaServico;
        }

        public async Task<Response<string>> Login(UsuarioLoginDto usuarioLogin)
        {
            Response<string> respostaServico = new Response<string>();
            try
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(userBanco => userBanco.Email == usuarioLogin.Email);
                
                if (usuario == null) 
                {
                    respostaServico.Mensagem = "Credenciais inválidas";
                    respostaServico.Status = false;
                    return respostaServico;
                }

                if(!_senhaInterface.VerificaSenhaHash(usuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    respostaServico.Mensagem = "Credenciais inválidas";
                    respostaServico.Status = false;
                    return respostaServico;
                }

                var token = _senhaInterface.CriarToken(usuario);

                respostaServico.Dados = token;
                respostaServico.Mensagem = "Usuário logado com sucesso";

            } catch (Exception ex) 
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = ex.Message;
                respostaServico.Status = false;
            }
            return respostaServico;
        }

        public bool VerificaSeEmailEUsuarioJaExistem(UsuarioCriacaoDto usuarioRegistro)
        {
            var usuario = _context.Usuario.FirstOrDefault(userBanco => userBanco.Email == usuarioRegistro.Email ||
            userBanco.Usuario == usuarioRegistro.Usuario);
            if (usuario != null)
            {
                return false;
            }
            return true;
        }
    }
}
