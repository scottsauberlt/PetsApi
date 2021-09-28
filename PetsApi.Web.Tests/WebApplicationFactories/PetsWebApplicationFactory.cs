using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PetsApi.Data.Models;
using PetsApi.Data.Repos;
using PetsApi.Web.Tests.TestServices;

namespace PetsApi.Web.Tests.WebApplicationFactories
{
    public class PetsWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<IPetsRepo>(serv =>
                {
                    var mockPetsRepo = new Mock<IPetsRepo>();
                    mockPetsRepo.Setup(x => x.GetPetById(3)).ReturnsAsync(new Pet { Id = 3});
                    mockPetsRepo.Setup(x => x.GetAll()).ReturnsAsync(new List<Pet>{new  Pet { Id = 3}, new Pet { Id = 4}});
                    return mockPetsRepo.Object;
                });
            });
        }
    }
}