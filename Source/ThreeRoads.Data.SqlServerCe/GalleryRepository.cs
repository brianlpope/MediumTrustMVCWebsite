using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.SqlServerCe
{
    public class GalleryRepository : Repository<Gallery>, IGalleryRepository
    {
        public GalleryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public IEnumerable<Gallery> GetGallery(string docPath)
        {
            IList<Gallery> rvalues = new List<Gallery>();
            IEnumerable<Gallery> dbGalleries = _unitOfWork.Set<Gallery>().AsEnumerable();
            DirectoryInfo di = new DirectoryInfo(docPath);
            FileInfo[] galleries = di.GetFiles();
            string galleryName=docPath.Substring(docPath.LastIndexOf("\\")+1);
            
            foreach (var gallery in galleries)
            {
                int delIndex = gallery.Name.LastIndexOf(".");
                if (delIndex > -1)
                {
                    Gallery g = new Gallery { Title=gallery.Name.Substring(0, delIndex), FileName=gallery.Name, GalleryName=galleryName, FileType=gallery.Name.Substring(delIndex + 1) };
                    //gallery.FileType = gallery.FileName.Substring(delIndex + 1);
                    rvalues.Add(g);
                }
               // gallery.FileName = string.Concat("\\", docPath, "\\", gallery.FileName.ToLower());              
            }

            return rvalues.AsEnumerable<Gallery>();
        }
    }
}
