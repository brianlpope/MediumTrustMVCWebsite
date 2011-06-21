using ThreeRoads.Data.Models;
using System.Collections.Generic;

namespace ThreeRoads.Data.Contracts
{
    public interface IGalleryRepository : IRepository<Gallery>
    {
        IEnumerable<Gallery> GetGallery(string docPath);
    }
}
