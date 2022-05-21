using _mvcproject_.Models;
using _mvcproject_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _mvcproject_.Controllers
{
    public class ProviderController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private OurContext context;
        private IOrderRepository orderRepo;
        private IWorksRepository worksRepository;
        public ProviderController(OurContext _context , UserManager<ApplicationUser> _userManager  , IWorksRepository worksRepository , IOrderRepository orderRepository)
        {
            this.userManager = _userManager;
            context = _context;
            this.worksRepository = worksRepository;
            orderRepo = orderRepository;
        }
        [Authorize(Roles = "CLIENT")]
        public IActionResult GetAll()
        {
            ViewBag.works = context.ProviderWorks;
            return View(userManager.Users.Where(u=>u.discriminator==1 && u.IsAccepted==1).Include(u=>u.Service).ToList());
        }
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult>  GetByServiceId(int id)
        {
            var client = User.Identity.Name;
            ViewBag.ClientUser = await userManager.FindByEmailAsync(client);
            return View(userManager.Users.Where(u => u.discriminator == 1 && u.Service_Id==id  && u.IsAccepted == 1).Include(u => u.Service).ToList());
        }

        [Authorize(Roles = "CLIENT")]
        public IActionResult Details(string id)
        {
            ViewBag.works = context.ProviderWorks.Where(p => p.Provider_Id == id).ToList();

            return View(userManager.Users.Where(u => u.Id==id).FirstOrDefault());
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult AdminIndex()
        {

            var users = userManager.Users.Where(o => o.Service_Id != null && o.IsAccepted == 0).Include(o=>o.Service).ToList();
            return View(users);
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult GetAcceptProvider()
        {

            var users = userManager.Users.Where(o => o.Service_Id != null && o.IsAccepted == 1).Include(o => o.Service).ToList();
            return View(users);
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetRejectProvider()
        {

            var users = userManager.Users.Where(o => o.Service_Id != null && o.IsAccepted == 2).Include(o => o.Service).ToList();
            return View(users);
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetBlockProvider()
        {

            var users = userManager.Users.Where(o => o.Service_Id != null && o.IsAccepted == 3).Include(o => o.Service).ToList();
            return View(users);
        }
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Accept(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            user.IsAccepted = 1;

            var updateuser = await userManager.UpdateAsync(user);

            return RedirectToAction("GetAcceptProvider");
        }
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Reject(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            user.IsAccepted = 2;

            var updateuser = await userManager.UpdateAsync(user);

            return RedirectToAction("GetRejectProvider");
        }
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Block(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            user.IsAccepted = 3;

            var updateuser = await userManager.UpdateAsync(user);

            return RedirectToAction("GetBlockProvider");
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult AdminDetails(string id)
        {
            ViewBag.works = context.ProviderWorks.Where(p => p.Provider_Id == id).ToList();
            var user = userManager.Users.Where(u => u.Id == id).Include(o => o.Service);
            return View(user);
        }
        [Authorize(Roles = "PROVIDER")]
        public async Task<IActionResult> Profile(string id)
        {
           
            var notification = orderRepo.GetNewByProviderId(id).Count();
            ViewBag.Notifications = notification;
            ViewBag.works = context.ProviderWorks.Where(p => p.Provider_Id == id).ToList();

            ApplicationUser user = await userManager.FindByIdAsync(id);
            return View(user);
        }
        [Authorize(Roles = "PROVIDER")]
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "PROVIDER")]
        public async Task<IActionResult> Edit(string id, ApplicationUser user)
        {

            ApplicationUser _user = await userManager.FindByIdAsync(id);

            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            _user.PhoneNumber = user.PhoneNumber;
            _user.Email = user.Email;
            _user.PriceStart = user.PriceStart;
            _user.PriceEnd = user.PriceEnd;
            _user.Gender = user.Gender;


            var result = await userManager.UpdateAsync(_user);
            if (result.Succeeded)
            {
                return RedirectToAction("Profile", new { Id = id });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
        }


    }
}
