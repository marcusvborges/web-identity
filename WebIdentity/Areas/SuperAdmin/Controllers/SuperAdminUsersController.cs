using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebIdentity.Areas.Admin.Controllers;

[Area("SuperAdmin")]
[Authorize(Roles = "SuperAdmin")]
public class SuperAdminUsersController : Controller
{
    private readonly UserManager<IdentityUser> userManager;

    public SuperAdminUsersController(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var users = userManager.Users;
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            ViewBag.ErrorMessage = $"User with Id = {id} not found";
            return View("NotFound");
        }
        else
        {
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index");
        }
    }
}
