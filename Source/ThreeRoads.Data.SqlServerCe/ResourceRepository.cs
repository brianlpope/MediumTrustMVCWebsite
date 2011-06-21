using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.SqlServerCe
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

    }
}
