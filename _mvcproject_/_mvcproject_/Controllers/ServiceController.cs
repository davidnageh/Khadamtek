using _mvcproject_.Models;
using _mvcproject_.Services;
using _mvcproject_.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _mvcproject_.Controllers
{
    public class ServiceController : Controller
    {
        private IServiceRepository serviceRepo;
        private IWebHostEnvironment env;
        public ServiceController(IServiceRepository serviceRepository, IWebHostEnvironment _env)
        {
            this.serviceRepo = serviceRepository;
            this.env = _env;
        }
        // [Authorize(Roles = "CLIENT")]

        public IActionResult GetAll()
        {
            return View(serviceRepo.GetAll());
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {
            return View(serviceRepo.GetAll());
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create(ServiceCreateViewModel ser)
        {
            try
            {
                Service service = new Service();
                service.Service_Name = ser.Service_Name;
                string imagepath = null;
                if (ser.Image != null)
                {
                    var path_1 = Path.Combine(env.WebRootPath, "servicelogos");
                    imagepath = Guid.NewGuid().ToString() + "_" + ser.Image.FileName;
                    var filepath = Path.Combine(path_1, imagepath);
                    using (var fs = new FileStream(filepath, FileMode.Create))
                    {
                        ser.Image.CopyTo(fs);
                    }
                }
                service.ImgPath = imagepath;
                serviceRepo.Create(service);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            Service ser = serviceRepo.Details(id);
            return View(ser);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                serviceRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult Edit(int id)
        {
            Service ser = serviceRepo.Details(id);
            return View(ser);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Edit(int id, Service service)
        {
            try
            {
                serviceRepo.Edit(id, service);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
