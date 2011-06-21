using System.Collections.Generic;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> FindByType(string type);
    }
}
