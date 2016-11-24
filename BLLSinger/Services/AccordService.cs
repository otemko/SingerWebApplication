using BLLSinger.Interfaces;
using DALSinger;
using ModelSinger;

namespace BLLSinger.Services
{
    public class AccordService : Service<Accord>, IAccordService
    {
        IRepository repository;

        public AccordService(IRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
