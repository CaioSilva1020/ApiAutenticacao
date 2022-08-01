using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AcessoDados
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        { }

        public MyDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-KDJJEAI\\SQLEXPRESS;Initial Catalog=ApiUsuario;Integrated Security=true", b => b.MigrationsAssembly("ApiAutenticacao"));
        }

        public DbSet<Login> Login { get; set; }
    }
}