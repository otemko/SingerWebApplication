using BLLSinger.Interfaces;
using ModelSinger;
using DALSinger;

namespace BLLSinger.Services
{
    public class SingerService : Service<Singer>, ISingerService
    {
        IRepository repository;

        public SingerService(IRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
