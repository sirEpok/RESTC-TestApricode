using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ApricodeTest.Models
{
    public class InfoGameContext: DbContext
    {
        public InfoGameContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<InfoGame> InfoGames { get; set; } = null!;
    }
}
