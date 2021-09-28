using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PetsApi.Data.Models;
using PetsApi.Web.Tests.WebApplicationFactories;
using Xunit;

namespace PetsApi.Web.Tests
{
    public class PetsControllerOverrideIntegrationTests : IClassFixture<PetsWebApplicationFactory>
    {
        private readonly PetsWebApplicationFactory _webApplicationFactory;

        public PetsControllerOverrideIntegrationTests(PetsWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task GetShouldReturnSinglePetWhenIdIsRequested()
        {
            int id = 3;
            var client = _webApplicationFactory.CreateClient();

            var pet = await client.GetFromJsonAsync<Pet>($"/pets/{id}");

            Assert.Equal(id, pet?.Id);
        }

        [Fact]
        public async Task GetShouldReturnAllPetsWhenNoIdIsRequested()
        {
            var client = _webApplicationFactory.CreateClient();

            var pets = await client.GetFromJsonAsync<List<Pet>>($"/pets") ?? new List<Pet>();

            Assert.Equal(2, pets.Count);
            Assert.Contains(pets, x => x.Id == 3);
            Assert.Contains(pets, x => x.Id == 4);
        }

    }
}