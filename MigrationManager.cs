using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
    public static class MigrationManager
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var usersContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!usersContext.Users.Any())
                {
                    var userManager = serviceScope.ServiceProvider
                        .GetRequiredService<UserManager<ApplicationUser>>();

                    var user = new ApplicationUser { UserName = "Gaben" };
                    userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
                    userManager.AddClaimAsync(user, new Claim( JwtClaimTypes.Role, "Admin" )).GetAwaiter().GetResult();
                    userManager.AddClaimAsync(user, new Claim( JwtClaimTypes.Picture, "https://avatars0.githubusercontent.com/u/50329419?s=460&u=f4e2be8f44d778c157c12912b12267a8954a7bc1&v=4" )).GetAwaiter().GetResult();
                }
            }
        }
    }
}