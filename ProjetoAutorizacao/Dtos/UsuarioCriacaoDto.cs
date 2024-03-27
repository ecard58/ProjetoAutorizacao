using ProjetoAutorizacao.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAutorizacao.Dtos
{
    public class UsuarioCriacaoDto
    {
        [Required(ErrorMessage ="o campo 'usuário' é obrigatório")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "o campo 'e-mail' é obrigatório"), EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "o campo 'senha' é obrigatório")]
        public string Senha { get; set;}
        [Compare("Senha", ErrorMessage ="Senhas não coincidem")]
        public string ConfirmaSenha { get; set; }
        [Required(ErrorMessage = "o campo 'cargo' é obrigatório")]
        public CargoEnum Cargo { get; set;}
    }
}
