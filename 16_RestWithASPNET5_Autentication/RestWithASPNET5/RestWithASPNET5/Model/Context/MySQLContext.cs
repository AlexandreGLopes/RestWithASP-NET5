using Microsoft.EntityFrameworkCore;

namespace RestWithASPNET5.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {

        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {

        }

        // Quando criamos a Classe precisamos adicioná-la ao contexto
        public DbSet<Person> Persons { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
