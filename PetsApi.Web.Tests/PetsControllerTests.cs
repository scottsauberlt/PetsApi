using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetsApi.Data;
using PetsApi.Data.Models;
using Xunit;

namespace PetsApi.Web.Tests
{
    public class PetsControllerTests
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;
        private readonly PetsDbContext _dbContext;

        public PetsControllerTests()
        {
            _webApplicationFactory = new WebApplicationFactory<Program>();
            _dbContext = _webApplicationFactory.Services.CreateScope().ServiceProvider.GetRequiredService<PetsDbContext>();
        }

        [Fact]
        public async Task GetShouldReturnSinglePetWhenIdIsRequested()
        {
            var id = 1;
            await CreatePet(id);
            var client = _webApplicationFactory.CreateClient();

            var pet = await client.GetFromJsonAsync<Pet>($"/api/pets/{id}");

            Assert.Equal(pet?.Id, id);
        }

        private async Task CreatePet(int id)
        {
            await _dbContext.Pets.ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
            _dbContext.Pets.Add(new Pet {Id = id});
            await _dbContext.SaveChangesAsync();
        }
    }
}