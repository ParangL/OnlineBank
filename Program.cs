using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BankAppMvc.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BankAppMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            try
            {
                var scope = host.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ctx.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if (!ctx.Roles.Any(role => role.Name == "Admin"))
                {
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                var cashierRole = new IdentityRole("Cashier");
                if (!ctx.Roles.Any(role => role.Name == "Cashier"))
                {
                    roleMgr.CreateAsync(cashierRole).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "parang"))
                {
                    //create an admin
                    var adminUser = new ApplicationUser
                    {
                        UserName = "parang"

                    };
                    var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                    //add role to user
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "cashier"))
                {
                    var cashierUser = new ApplicationUser
                    {
                        UserName = "cashier"
                    };
                    var result = userMgr.CreateAsync(cashierUser, "password").GetAwaiter().GetResult();
                    //add role to user
                    userMgr.AddToRoleAsync(cashierUser, cashierRole.Name).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}




