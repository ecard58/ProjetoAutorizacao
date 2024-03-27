using Microsoft.EntityFrameworkCore;
using ProjetoAutorizacao.Models;

namespace ProjetoAutorizacao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UsuarioModel> Usuario { get; set; }
    }
}
