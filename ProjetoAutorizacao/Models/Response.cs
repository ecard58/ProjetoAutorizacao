namespace ProjetoAutorizacao.Models
{
    public class Response<T>
    {
        public T? Dados { get; set; }
        public bool Status { get; set; } = true;
        public string Mensagem { get; set; } = string.Empty;
    }
}
