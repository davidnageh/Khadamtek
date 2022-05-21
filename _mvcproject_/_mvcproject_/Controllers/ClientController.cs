using _mvcproject_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace _mvcproject_.Controllers
{
    public class ClientController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IHostingEnvironment env;
        private OurContext context;

        public ClientController(UserManager<ApplicationUser> userManager , IHostingEnvironment hostingEnvironment , OurContext _context)
        {
            this.userManager = userManager;
            this.env = hostingEnvironment;
            context = _context;
        }

        [Authorize(Roles ="CLIENT")]
        public async Task<IActionResult> Details(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            ViewBag.orders = context.Orders.Where(o => o.Client_Id == id).Include(o => o.Provider).ToList();

            return View(user);
        }
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ApplicationUser model) 
        {
            //string imagename = null;
            //if(model.profile != null)
            //{
            //    var path = Path.Combine(env.WebRootPath, "Images");
            //    imagename = Guid.NewGuid().ToString() + "_" + model.profile.FileName;
            //    var fullpath= Path.Combine(path,imagename);
            //    model.profile.CopyTo(new FileStream(fullpath, FileMode.Create));
            //}
            ApplicationUser user =await userManager.FindByIdAsync(id);  
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = model.Gender;

            var result =await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("details" , new { id = id });
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);                   
                }
                return View();  
            }
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult AdminIndex()
        {
            var users = userManager.Users.Where(o => o.Service_Id == null).ToList();
            return View(users);
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult AdminDetails(string id)
        {
            var user = userManager.Users.Where(u => u.Id == id);
            return View(user);
        }
    }
}
