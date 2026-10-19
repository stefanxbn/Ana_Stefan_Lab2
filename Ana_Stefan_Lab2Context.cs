using Microsoft.EntityFrameworkCore;

public class Ana_Stefan_Lab2Context(DbContextOptions<Ana_Stefan_Lab2Context> options) : DbContext(options)
{
    public DbSet<Ana_Stefan_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Ana_Stefan_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Ana_Stefan_Lab2.Models.Author> Author { get; set; } = default!;
}
