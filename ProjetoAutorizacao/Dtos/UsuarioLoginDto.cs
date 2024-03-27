using System.ComponentModel.DataAnnotations;

namespace ProjetoAutorizacao.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "o campo 'e-mail' é obrigatório"), EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "o campo 'senha' é obrigatório")]
        public string Senha { get; set; }
    }
}
