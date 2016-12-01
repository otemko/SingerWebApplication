using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Accord>> GetAccordsByAccordNames(string[] accordNames)
        {
            var allAccords = new List<Accord>();

            foreach (var accordName in accordNames)
            {
                var accords = await repository.Get<Accord>(a => a.Name == accordName);
                allAccords.AddRange(accords);
            }

            return allAccords;
        }
    }
}
