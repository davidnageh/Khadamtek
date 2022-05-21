using _mvcproject_.Models;

namespace _mvcproject_.Services
{
    public interface IOrderRepository
    {
        public List<Order> GetAll();
        public List<Order> GetByClientId(string id);
        public List<Order> GetByProviderId (string id);
        public List<Order> GetRejectedByProviderId(string id);
        public List<Order> GetAcceptedByProviderId(string id);
        public List<Order> GetNewByProviderId(string id);
        public Order Details(int id);
         public void Edit(int id , Order order);
         public void Delete(int id);
         public void Create(Order o);


    }
}
