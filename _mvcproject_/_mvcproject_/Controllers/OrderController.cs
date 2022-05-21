using _mvcproject_.Models;
using _mvcproject_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _mvcproject_.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository orderRepo;
        private UserManager<ApplicationUser> userManager;
        public OrderController(IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
        {
            this.orderRepo = orderRepository;
            this.userManager = userManager;
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {

            return View(orderRepo.GetAll());
        }
        [Authorize(Roles = "CLIENT")]
        public IActionResult GetByClientId(string id)
        {
            return View(orderRepo.GetByClientId(id));
        }

        [Authorize(Roles = "CLIENT")]
        public IActionResult Details(int id)
        {
            return View(orderRepo.Details(id));
        }
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> Create(string id)
        {
            ViewBag.ClientId = (await userManager.FindByEmailAsync(User.Identity.Name)).Id;
            ViewBag.ProviderId = id;
            return PartialView("_CreateOrderPartial");
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                order.IsAccepted = 0;
                orderRepo.Create(order);
                return RedirectToAction("GetAll", "service");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Order was not completed , check your data again");
                return View();
            }
           
        }
        [HttpPost]
        [Authorize(Roles ="CLIENT")]
        public IActionResult Delete(int id)
        {
            orderRepo.Delete(id);
            return RedirectToAction("GetAll", "services");
        }

        [Authorize(Roles = "CLIENT")]
        public IActionResult Edit(int id)
        {
            return PartialView("_EditOrderPartial", orderRepo.GetAll().Where(o => o.Id == id).FirstOrDefault());
        }
        [HttpPost]
        [Authorize(Roles = "CLIENT")]
        public IActionResult Edit(int id, Order O)
        {
            var order = orderRepo.GetAll().Where(o => o.Id == id).FirstOrDefault();
            //  if(order.IsAccepted == 1)
            {
                order.Rating = O.Rating;
                order.Complaint = O.Complaint;
                orderRepo.Edit(id, order);
            }

            return RedirectToAction("GetByClientId", new { id = order.Client_Id });
        }
        [Authorize(Roles = "PROVIDER")]
        public IActionResult GetAcceptOrdersByProviderId(string id)
        {
            var AccOrders = orderRepo.GetAcceptedByProviderId(id);
            return View(AccOrders);
        }
        [Authorize(Roles = "PROVIDER")]
        public IActionResult GetRejectOrderByProviderId(string id)
        {

            var RejOrders = orderRepo.GetRejectedByProviderId(id);
            return View(RejOrders);
        }
         [Authorize(Roles = "PROVIDER")]
        public IActionResult GetNewOrdersByProviderId(string id)
        {

            var NewOrders = orderRepo.GetNewByProviderId(id);
            return View(NewOrders);
        }
        [Authorize(Roles = "PROVIDER")]
        public async Task<IActionResult> Reject(int id)
        {
            var order = orderRepo.GetAll().Where(o => o.Id == id).FirstOrDefault();

            order.IsAccepted = 2;

            orderRepo.Edit(id, order);

            return RedirectToAction("GetNewOrdersByProviderId", new { Id = order.Provider_Id });
        }

        [Authorize(Roles = "PROVIDER")]
        public async Task<IActionResult> Accept(int id)
        {
            var order = orderRepo.GetAll().Where(o => o.Id == id).FirstOrDefault();

            order.IsAccepted = 1;

            orderRepo.Edit(id, order);

            return RedirectToAction("GetNewOrdersByProviderId", new { Id = order.Provider_Id });
        }
    }
}
