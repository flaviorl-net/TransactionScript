using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions options) : base(options) { }

    public DbSet<Cliente> Cliente { get; set; }

    public DbSet<EnderecoCliente> EnderecoCliente { get; set; }
}