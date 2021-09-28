using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PetsApi.Data.Models;
using PetsApi.Web.Tests.WebApplicationFactories;
using Xunit;

namespace PetsApi.Web.Tests
{
    public class AuthPetsControllerIntegrationTests : IClassFixture<AuthPetsWebApplicationFactory>
    {
        private readonly AuthPetsWebApplicationFactory _webApplicationFactory;

        public AuthPetsControllerIntegrationTests(AuthPetsWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task GetShouldReturnSinglePetWhenIdIsRequested()
        {
            int id = 3;
            var client = _webApplicationFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme);

            var pet = await client.GetFromJsonAsync<Pet>($"/authpets/{id}");

            Assert.Equal(id, pet?.Id);
        }

        [Fact]
        public async Task GetShouldReturnAllPetsWhenNoIdIsRequested()
        {
            var client = _webApplicationFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme);

            var pets = await client.GetFromJsonAsync<List<Pet>>($"/authpets") ?? new List<Pet>();

            Assert.Equal(2, pets.Count);
            Assert.Contains(pets, x => x.Id == 3);
            Assert.Contains(pets, x => x.Id == 4);
        }

    }
}