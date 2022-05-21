using _mvcproject_.Models;
using Microsoft.EntityFrameworkCore;

namespace _mvcproject_.Services
{
    public class OrderRepoService : IOrderRepository
    {
        private OurContext context;
        public OrderRepoService(OurContext _context)
        {
            this.context = _context;
        }
        public void Create(Order o)
        {
            context.Orders.Add(o);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
           Order o = context.Orders.Find(id);
            context.Orders.Remove(o);
            context.SaveChanges();
        }

        public Order Details(int id)
        {
            return context.Orders.Find(id);
        }

        public void Edit(int id, Order order)
        {
            Order o = context.Orders.Find(id);
            if(o != null)
            {
                o.Complaint = order.Complaint;
                o.Rating = order.Rating;
                o.Message = order.Message;
                o.IsAccepted = order.IsAccepted;
                context.SaveChanges();
            }
        }

        public List<Order> GetAll()
        {
            return context.Orders.Include(o => o.Provider).Include(o=>o.User).ToList(); 
        }

        public List<Order> GetByClientId(string id)
        {
            List<Order> orders = context.Orders.Where(o => o.Client_Id == id).Include(o => o.Provider).ToList();
            return orders;
        }

        public List<Order> GetByProviderId(string id)
        {
            List<Order> orders = context.Orders.Where(o => o.Provider_Id == id).ToList();
            return orders;
        }

        public List<Order> GetNewByProviderId(string id)
        {
            List<Order> orders = context.Orders.Where(o => o.Provider_Id == id && o.IsAccepted == 0).Include(o => o.User).Include(o => o.Provider).ToList();
            return orders;
        }

        public List<Order> GetRejectedByProviderId(string id)
        {
            List<Order> orders = context.Orders.Where(o => o.Provider_Id == id && o.IsAccepted == 2).Include(o => o.User).Include(o=>o.Provider).ToList();
            return orders;
        }

        public List<Order> GetAcceptedByProviderId(string id)
        {
            List<Order> orders = context.Orders.Where(o => o.Provider_Id == id && o.IsAccepted == 1).Include(o => o.User).Include(o => o.Provider).ToList();
            return orders;
        }
    }
}
