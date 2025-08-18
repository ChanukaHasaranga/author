using author.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorDBContext

{
    public class AuthordbContext : DbContext
    {

        public DbSet<Author> Author { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionstring = @"Server=LAPTOP-JCD3PTT6\SQL2022NEW; Database=TestDB1; Trusted_Connection=True; TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionstring);

        }



    }
}
