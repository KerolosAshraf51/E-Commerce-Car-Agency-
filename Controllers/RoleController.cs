using CarAgency.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarAgency.Controllers
{

    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> roleMan;

    

        public RoleController(RoleManager<IdentityRole<int>> _roleMan)
        {
            roleMan = _roleMan;
        }
        public IActionResult Role()
        {
            return View("addRole");
        }

        public async Task<IActionResult> addRole(roleVM rvm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole<int> role = new IdentityRole<int>();
                role.Name = rvm.Name;

                IdentityResult res = await roleMan.CreateAsync(role);

                if (res.Succeeded) 
                {
                    ViewBag.success=true;
                    return View("addRole");
                }

                foreach (var e in res.Errors)
                {
                    ModelState.AddModelError("", e.Description);
                }

            }

            return View("addRole",rvm);
        }
    }
}
