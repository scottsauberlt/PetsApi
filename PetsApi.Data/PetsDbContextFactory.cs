using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PetsApi.Data
{
    public class PetsDbContextFactory : IDesignTimeDbContextFactory<PetsDbContext>
    {
        public PetsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PetsDbContext>();
            optionsBuilder.UsePetsSqlLite();
            return new PetsDbContext(optionsBuilder.Options);
        }
    }
}