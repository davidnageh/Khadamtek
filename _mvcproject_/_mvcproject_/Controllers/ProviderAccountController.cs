using _mvcproject_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Authorization;

namespace _mvcproject_.Controllers
{
    [AllowAnonymous]
    public class ProviderAccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private OurContext context;
        private IWebHostEnvironment hostEnvironment;

        public ProviderAccountController(OurContext _context,UserManager<ApplicationUser> _userManager, 
            SignInManager<ApplicationUser> _signInManager , IWebHostEnvironment _hostEnvironment)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            context = _context;
            hostEnvironment = _hostEnvironment;
        }
        public IActionResult Register()
        {
            ViewBag.services = context.Services.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(ClientRegisterViewModel model)
        {
            string nationalidPath = null;
            string previousworkPath = null;
            List<string> images = new List<string>();
            if (model.nationalidimage != null)
            {
                string path_1 = Path.Combine(hostEnvironment.WebRootPath, "images");
                nationalidPath = Guid.NewGuid().ToString() + "_" + model.nationalidimage.FileName;
                var filepath= Path.Combine(path_1, nationalidPath);
                using(var file =new FileStream(filepath, FileMode.Create)){
                    model.nationalidimage.CopyTo(file);
                }
                

            }
            if (model.workimage != null && model.workimage.Count > 0)
            {
               
                foreach(var image in model.workimage)
                {
                    string path_1 = Path.Combine(hostEnvironment.WebRootPath, "images");
                    previousworkPath = Guid.NewGuid().ToString() + "_" + image.FileName;
                    images.Add(previousworkPath);
                    var filepath = Path.Combine(path_1, previousworkPath);
                    using (var file = new FileStream(filepath, FileMode.Create))
                    {
                        image.CopyTo(file);
                    }
                }
                
            }
            ApplicationUser apuser = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                discriminator = 1,
                UserName = model.Email,
                NationalId = model.NationalId,
                NationalIdPhoto = nationalidPath,
                IsAccepted = 0 ,
                Service_Id = model.Service_id,
                PriceStart=model.PriceStart,
                PriceEnd=model.PriceEnd,
                
            };

           
            var result = await userManager.CreateAsync(apuser, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(apuser, "PROVIDER");
                await signInManager.SignInAsync(apuser, isPersistent: false);
                ApplicationUser user = await userManager.FindByEmailAsync(apuser.Email);
                foreach (var image in images)
                {                  
                    ProviderWorks providerWorks = new ProviderWorks()
                    {
                        Provider_Id = user.Id  ,
                        ImgPath = image
                    };
                    context.ProviderWorks.Add(providerWorks);
                    context.SaveChanges();
                }
                return RedirectToAction("login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(ProviderLoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.password, false, false);
            var user = (await userManager.FindByEmailAsync(model.UserName));
            if (result.Succeeded && user.IsAccepted==1)
            {
                return RedirectToAction("profile", "provider" , new { id= user.Id});
            }
            else if(result.Succeeded && user.IsAccepted == 0)
            {
                return View("pending");
            }
            else if(result.Succeeded && user.IsAccepted == 2)
            {
                return View("blocked");
            }
            ModelState.AddModelError(string.Empty, result.ToString());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("getall", "service");
        }
    }
}
