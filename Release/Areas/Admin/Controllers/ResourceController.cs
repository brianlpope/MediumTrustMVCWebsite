using System.IO;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThreeRoads.MVC.Controllers;
using ThreeRoads.Data.SqlServerCe;
using ThreeRoads.Data.Models;
using ThreeRoads.Data.Contracts;

namespace ThreeRoads.MVC.Areas.Admin.Controllers
{
    public class ResourceController : BaseController
    {
        private IResourceRepository _resourceRepository;
        private ICategoryRepository _categoryRepository;

        public ResourceController()
        {
            _resourceRepository = new ResourceRepository(UnitOfWork);
            _categoryRepository = new CategoryRepository(UnitOfWork);
        }

        public ResourceController(IUnitOfWork unitOfWork, IResourceRepository ResourceRepository, ICategoryRepository categoryRepository)
            : base(unitOfWork)
        {
            _resourceRepository = ResourceRepository;
            _categoryRepository = categoryRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(_resourceRepository.GetAll());
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Resource resource = _resourceRepository.GetById(id);
            resource.Description = resource.Description.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
            ViewData["categories"] = new SelectList(_categoryRepository.FindByType("resource"), "ID", "Name",resource.Category.ID);

            return View(resource);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, Resource Resource, int categoryId)
        {
            var ResourceInDb = _resourceRepository.GetById(id);

            ResourceInDb.Category = _categoryRepository.GetById(categoryId);


            if (TryUpdateModel(ResourceInDb))
            {
                UnitOfWork.Save();
                return RedirectToAction("Index", "Resource");
            }

            return View(Resource);
        }
        [Authorize]
        public ActionResult Create(string topic)
        {
            Resource Resource = new Resource();
            Resource.Topic = topic;

            ViewData["categories"] = new SelectList(_categoryRepository.FindByType("resource"), "ID", "Name", _categoryRepository.GetByIndex(0));

            return View(Resource);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Resource Resource, int categoryId)
        {
            if (ModelState.IsValid)
            {
                Resource.Category = _categoryRepository.GetById(categoryId);

                UpdateModel<Resource>(Resource);

                _resourceRepository.Add(Resource);
                UnitOfWork.Save();
                return RedirectToAction("Index", "Resource"); 
            }

            return View(Resource);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Resource Resource = _resourceRepository.GetById(id);

            if (Resource == null)
                return View("NotFound");
            else
                return View(Resource);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            Resource Resource = _resourceRepository.GetById(id);
            
            if (Resource != null)
            {
                _resourceRepository.Remove(Resource);
                UnitOfWork.Save();
            }

            return RedirectToAction("Index", "Resource");
        }
    }
}
