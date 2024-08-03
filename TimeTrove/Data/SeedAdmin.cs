using Microsoft.AspNetCore.Identity;

namespace TimeTrove.Data;

public static class SeedAdmin
{

    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        var appAdmin = await userManager.FindByNameAsync(config["superAdminCredentials:Username"]);
        if (appAdmin == null)
        {

            appAdmin = new ApplicationUser()
            {
                Email = config["superAdmincredentials:Email"],
                NormalizedEmail = config["superAdmincredentials:Email"].ToUpper(),
                EmailConfirmed = true,
                UserName = config["superAdminCredentials:Username"],
                NormalizedUserName = config["superAdminCredentials:Username"].ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),

            };

            try
            {
                await userManager.CreateAsync(appAdmin, config["superAdminCredentials:Password"]);


                var user = await userManager.FindByIdAsync(appAdmin.Id);
            }
            catch (Exception)
            {

                throw;
            }

            userManager.AddToRoleAsync(appAdmin, "Admin").Wait();

        }
        else
        {
            if (!await userManager.IsInRoleAsync(appAdmin, "Admin"))
            {
                userManager.AddToRoleAsync(appAdmin, "Admin").Wait();
            }
        }
    }
}