using System.Linq;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.SqlServerCe
{
    public class BioRepository : Repository<Bio>, IBioRepository
    {
        public BioRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public IQueryable<Models.Bio> GetByCategoryId(int categoryId)
        {
            return _unitOfWork.Bios.Where(b => b.Category.ID == categoryId);
        }
        public IQueryable<Models.Bio> GetStaff()
        {
            return GetByCategoryId(1);
        }
        public IQueryable<Models.Bio> GetBoard()
        {
            return GetByCategoryId(2);
        }
    }
}
