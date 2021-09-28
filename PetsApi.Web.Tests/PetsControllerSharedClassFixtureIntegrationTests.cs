using System.Collections.Generic;
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
    public class PetsControllerSharedClassFixtureIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private readonly PetsDbContext _dbContext;

        public PetsControllerSharedClassFixtureIntegrationTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _dbContext = _webApplicationFactory.Services.CreateScope().ServiceProvider.GetRequiredService<PetsDbContext>();
            RemoveAllPets();
        }

        [Fact]
        public async Task GetShouldReturnSinglePetWhenIdIsRequested()
        {
            var id = 1;
            CreatePet(1);
            var client = _webApplicationFactory.CreateClient();

            var pet = await client.GetFromJsonAsync<Pet>($"/pets/{id}");

            Assert.Equal(pet?.Id, id);
        }

        [Fact]
        public async Task GetShouldReturnAllPetsWhenNoIdIsRequested()
        {
            CreatePet(1);
            CreatePet(2);
            var client = _webApplicationFactory.CreateClient();

            var pets = await client.GetFromJsonAsync<List<Pet>>($"/pets") ?? new List<Pet>();

            Assert.Equal(2, pets.Count);
            Assert.Contains(pets, x => x.Id == 1);
            Assert.Contains(pets, x => x.Id == 2);
        }

        private void CreatePet(int id)
        {
            _dbContext.SaveChanges();
            _dbContext.Pets.Add(new Pet {Id = id});
            _dbContext.SaveChanges();
        }

        private void RemoveAllPets()
        {
            _dbContext.Pets.RemoveRange(_dbContext.Pets.ToList());
        }
    }
}