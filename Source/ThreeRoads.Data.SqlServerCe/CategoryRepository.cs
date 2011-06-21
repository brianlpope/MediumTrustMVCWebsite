using System.Collections.Generic;
using System.Linq;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.SqlServerCe
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Category> FindByType(string type)
        {
            return _unitOfWork.Categories.Where(c => c.Type == type);
        }
    }
}
