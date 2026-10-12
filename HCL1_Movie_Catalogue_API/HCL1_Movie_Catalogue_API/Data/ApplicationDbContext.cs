
using HCL1_Movie_Catalogue_API.Models;
using Microsoft.EntityFrameworkCore;

namespace HCL1_Movie_Catalogue_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;
    }
}
