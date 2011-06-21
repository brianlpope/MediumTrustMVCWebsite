using System.Linq;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.Contracts
{
    public interface IEventRepository : IRepository<Event>
    {
        IQueryable<Event> GetByType(string categoryType);
    }
}

