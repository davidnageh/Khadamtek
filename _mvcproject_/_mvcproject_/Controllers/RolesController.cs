using _mvcproject_.Models;
using _mvcproject_.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _mvcproject_.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public RolesController(RoleManager<IdentityRole> _roleManager 
                             , UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        // GET: RolesController

       
        public ActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }


        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel r)
        {
                IdentityRole role = new IdentityRole();
                role.Name = r.RoleName;
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                    return View(r);  
                }              

        }

        // GET: RolesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var users = userManager.Users;
            List<string> userinrole = new List<string>();
            foreach(var user in users)
            {
                if(await userManager.IsInRoleAsync(user , role.Name))
                {
                    userinrole.Add(user.UserName);
                }
            }
            ViewBag.users= userinrole;
            return View(role);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IdentityRole formrole)
        {
            var role = await roleManager.FindByIdAsync(id);
            role.Name = formrole.Name;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(id,formrole);             
            }

        }


    }
}
