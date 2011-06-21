using System.Collections.Generic;
using System.Web.Mvc;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.SqlServerCe;
using System.Configuration;
using ThreeRoads.Data.Models;
using System.IO;

namespace ThreeRoads.MVC.Controllers
{
    public class GalleryController : BaseController
    {
        private IGalleryRepository _galleryRepository;

        public GalleryController()
        {
            _galleryRepository = new GalleryRepository(UnitOfWork);
        }

        public GalleryController(IUnitOfWork unitOfWork, IGalleryRepository galleryRepository)
            : base(unitOfWork)
        {
            _galleryRepository = galleryRepository;
        }
        

        // get all galleries
        public ActionResult Galleries()
        {
            string galleryPath = ConfigurationManager.AppSettings["GalleryPath"];
            galleryPath = Server.MapPath(galleryPath);
            IEnumerable<Gallery> gallery = _galleryRepository.GetGallery(galleryPath);

            return View(gallery);
        }
        // get specific gallery with page number
        public ActionResult Gallery(string name, int pageIndex)
        {
            string galleryPath = ConfigurationManager.AppSettings["GalleryPath"];
            galleryPath = Path.Combine(Server.MapPath(galleryPath), name);
            IEnumerable<Gallery> gallery = _galleryRepository.GetGallery(galleryPath);

            return View(gallery);

        }
    }

}
