using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;
using System.Linq;
using System.Collections.Generic;

namespace ThreeRoads.Data.SqlServerCe
{
    public class DownloadRepository : Repository<Download>, IDownloadRepository
    {
        public DownloadRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public IEnumerable<Download> GetPathedList(string docPath)
        {
            IEnumerable<Download> downloads = _unitOfWork.Set<Download>().AsEnumerable();
            foreach (var download in downloads)
            {
                int delIndex = download.FileName.LastIndexOf(".");
                if (delIndex > -1)
                {
                    download.FileType = download.FileName.Substring(delIndex + 1);
                }
                download.FileName = string.Concat("\\", docPath, "\\", download.FileName.ToLower());              
            }

            return downloads;
        }
    }
}
