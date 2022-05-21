using _mvcproject_.Models;

namespace _mvcproject_.Services
{
    public interface IServiceRepository
    {
        public List<Service> GetAll();
        public Service Details(int id);
        public void Edit(int id, Service service);
        public void Delete(int id);
        public void Create(Service s);
    }
}
