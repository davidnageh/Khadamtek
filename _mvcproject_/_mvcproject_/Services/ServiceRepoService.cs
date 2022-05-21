using _mvcproject_.Models;

namespace _mvcproject_.Services
{
    public class ServiceRepoService : IServiceRepository
    {
        private OurContext context;
        public ServiceRepoService(OurContext _context)
        {
            this.context = _context;
        }
        public void Create(Service s)
        {
            context.Services.Add(s);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Service s = context.Services.Find(id);
            context.Services.Remove(s);
            context.SaveChanges();
        }

        public Service Details(int id)
        {
            return context.Services.Find(id);
        }

        public void Edit(int id, Service service)
        {
            Service s = context.Services.Find(id);
            if (s != null)
            {
                s.ImgPath = service.ImgPath;
                s.Service_Name = service.Service_Name;
            }
            context.SaveChanges();
        }

        public List<Service> GetAll()
        {
            return context.Services.ToList();
        }
    }
}
