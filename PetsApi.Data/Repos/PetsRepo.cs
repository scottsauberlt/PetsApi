using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetsApi.Data.Models;

namespace PetsApi.Data.Repos
{
    public interface IPetsRepo
    {
        Task<Pet> GetPetById(int id);
        Task<List<Pet>> GetAll();
    }

    public class PetsRepo : IPetsRepo
    {
        private readonly PetsDbContext _dbContext;

        public PetsRepo(PetsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Pet> GetPetById(int id) => _dbContext.Pets.SingleOrDefaultAsync(x => x.Id == id);
        public Task<List<Pet>> GetAll() => _dbContext.Pets.ToListAsync();
    }
}