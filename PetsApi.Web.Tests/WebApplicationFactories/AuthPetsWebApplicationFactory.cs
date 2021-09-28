using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using PetsApi.Data.Repos;
using PetsApi.Web.Constants;
using PetsApi.Web.Tests.TestServices;

namespace PetsApi.Web.Tests.WebApplicationFactories
{
    public class AuthPetsWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        JwtBearerDefaults.AuthenticationScheme, options => { });
                services.AddScoped<IPetsRepo, FakePetsRepo>();
            });
            builder.UseEnvironment(PetsApiEnvironments.AuthTests);
        }
    }
}