using Microsoft.AspNetCore.Identity;

namespace WebIdentity.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedRoleAsync()
        {
            if(!await _roleManager.RoleExistsAsync("User"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "SuperAdmin";
                role.NormalizedName = "SUPERADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUserAsync()
        {
            if (await _userManager.FindByEmailAsync("user@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "user@localhost";
                user.Email = "user@localhost";
                user.NormalizedUserName = "USER@LOCALHOST";
                user.NormalizedEmail = "USER@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "DefaultUser@112233");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }
            
            if (await _userManager.FindByEmailAsync("admin@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "DefaultAdmin@112233");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            if (await _userManager.FindByEmailAsync("superadmin@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "superadmin@localhost";
                user.Email = "superadmin@localhost";
                user.NormalizedUserName = "SUPERADMIN@LOCALHOST";
                user.NormalizedEmail = "SUPERADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "DefaultSuperAdmin@112233");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "SuperAdmin");
                }
            }
        }

    }
}
