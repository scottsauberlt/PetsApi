using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
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
                services.AddScoped<IPetsRepo, FakePetsRepo>();
            });
        }
    }
}