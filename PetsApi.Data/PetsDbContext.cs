using Microsoft.EntityFrameworkCore;
using PetsApi.Data.Models;

namespace PetsApi.Data
{
    public class PetsDbContext : DbContext
    {
        public PetsDbContext(DbContextOptions<PetsDbContext> options) : base(options)
        {
        }
        
        public DbSet<Pet> Pets { get; set; }
    }
}