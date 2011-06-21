using ThreeRoads.Data.Models;
using System.Collections.Generic;

namespace ThreeRoads.Data.Contracts
{
    public interface IDownloadRepository : IRepository<Download>
    {
        IEnumerable<Download> GetPathedList(string docPath);
    }
}
