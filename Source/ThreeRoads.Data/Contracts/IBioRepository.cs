using System.Linq;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.Contracts
{
    public interface IBioRepository : IRepository<Bio>
    {
        IQueryable<Bio> GetByCategoryId(int categoryId);
        IQueryable<Bio> GetStaff();
        IQueryable<Bio> GetBoard();
    }
}
