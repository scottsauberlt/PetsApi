using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PetsApi.Data;
using PetsApi.Data.Models;
using Xunit;

namespace PetsApi.Web.Tests
{
    public class PetsControllerTests
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private readonly PetsDbContext _dbContext;

        public PetsControllerTests()
        {
            _webApplicationFactory = new WebApplicationFactory<Startup>();
            _dbContext = _webApplicationFactory.Services.CreateScope().ServiceProvider.GetRequiredService<PetsDbContext>();
        }

        [Fact]
        public async Task GetShouldReturnSinglePetWhenIdIsRequested()
        {
            var id = 1;
            CreatePet(id);
            var client = _webApplicationFactory.CreateClient();

            var pet = await client.GetFromJsonAsync<Pet>($"/pets/{id}");

            Assert.Equal(pet?.Id, id);
        }

        private void CreatePet(int id)
        {
            _dbContext.Pets.RemoveRange(_dbContext.Pets.ToList());
            _dbContext.SaveChanges();
            _dbContext.Pets.Add(new Pet {Id = id});
            _dbContext.SaveChanges();
        }
    }
}