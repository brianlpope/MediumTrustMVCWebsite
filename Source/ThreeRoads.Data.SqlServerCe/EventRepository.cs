using System.Linq;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.SqlServerCe
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public IQueryable<Models.Event> GetByType(string categoryType)
        {
            return _unitOfWork.Events.Where(e => e.Category.Type == categoryType);
        }

    }
}
