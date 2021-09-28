using System.Collections.Generic;
using System.Threading.Tasks;
using PetsApi.Data.Models;
using PetsApi.Data.Repos;

namespace PetsApi.Web.Tests.TestServices
{
    class FakePetsRepo : IPetsRepo
    {
        public Task<Pet> GetPetById(int id)
        {
            return Task.FromResult(new Pet {Id = id});
        }

        public Task<List<Pet>> GetAll()
        {
            return Task.FromResult(new List<Pet> {new Pet {Id = 3}, new Pet { Id = 4}});
        }
    }
}