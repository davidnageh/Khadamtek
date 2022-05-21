using _mvcproject_.Models;

namespace _mvcproject_.Services
{
    public class WorksRepoService : IWorksRepository
    {
        private OurContext context;
        public WorksRepoService(OurContext _context)
        {
            this.context = _context;
        }
        public List<ProviderWorks> GetAll()
        {
            return context.ProviderWorks.ToList();
        }


    }

}