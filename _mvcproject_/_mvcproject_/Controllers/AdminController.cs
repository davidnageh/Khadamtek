using _mvcproject_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _mvcproject_.Controllers
{
    public class AdminController : Controller
    {


        private UserManager<ApplicationUser> userManager;
        private OurContext context;


        public AdminController(UserManager<ApplicationUser> _userManager, OurContext _context)
        {
            userManager = _userManager;
            context = _context;

        }
        [Authorize(Roles ="ADMIN")]
        public IActionResult Index()
        {
            ViewBag.providers = userManager.Users.Where(p => p.Service != null).Count();
            ViewBag.clients = userManager.Users.Where(p => p.Service == null).Count();
            ViewBag.services = context.Services.Count();
            ViewBag.orders = context.Orders.Count();
            return View();
        }
    }
}
