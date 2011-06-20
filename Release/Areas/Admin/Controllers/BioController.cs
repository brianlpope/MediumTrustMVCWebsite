using System.IO;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThreeRoads.MVC.Controllers;
using ThreeRoads.Data.Models;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.SqlServerCe;
using System.Collections.Generic;

namespace ThreeRoads.MVC.Areas.Admin.Controllers
{
    public class BioController : BaseController
    {
        private string _bioImagePath = ConfigurationManager.AppSettings["BioImagePath"];
        IBioRepository _bioRepository;
        ICategoryRepository _categoryRepository;

        public BioController(IUnitOfWork unitOfWork, IBioRepository bioRepository, ICategoryRepository categoryRepository) : base(unitOfWork)
        {
            _bioRepository = bioRepository;
            _categoryRepository = categoryRepository;
        }

        public BioController()
        {
            _bioRepository = new BioRepository(UnitOfWork);
            _categoryRepository = new CategoryRepository(UnitOfWork);
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(_bioRepository.GetAll());
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Bio bio = _bioRepository.GetById(id);
            bio.Description = bio.Description.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
            ViewData["categories"] = new SelectList(_categoryRepository.FindByType("bio"), "ID", "Name", bio.Category.ID);

            return View(bio);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, Bio bio, int categoryId, HttpPostedFileBase file)
        {
            var BioInDb = _bioRepository.GetById(id);

            BioInDb.Category = _categoryRepository.GetById(categoryId);

            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath(_bioImagePath), fileName));
                BioInDb.ImageName = fileName;
            }

            if (TryUpdateModel(BioInDb))
            {
                UnitOfWork.Save();
                return RedirectToAction("Index", "Bio");
            }

            return View(bio);
        }
        [Authorize]
        public ActionResult Create(string topic)
        {
            Bio bio = new Bio();
            bio.Topic = topic;
            IEnumerable<Category> categories = _categoryRepository.FindByType("bio");
            ViewData["categories"] = new SelectList(categories, "ID", "Name", categories.FirstOrDefault());

            return View(bio);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Bio bio, int categoryId, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string fileName = "no_image.gif";
                if (file != null)
                {
                    fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath(_bioImagePath), fileName));
                }
                bio.Category = _categoryRepository.GetById(categoryId);
                bio.ImageName = fileName;
                UpdateModel<Bio>(bio);

                _bioRepository.Add(bio);
                UnitOfWork.Save();
                return RedirectToAction("Index", "Bio"); // action = BoardAndStaff
            }

            return View(bio);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Bio bio = _bioRepository.GetById(id);

            if (bio == null)
                return View("NotFound");
            else
                return View(bio);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            Bio bio = _bioRepository.GetById(id);
            
            if (bio != null)
            {
                _bioRepository.Remove(bio);
                UnitOfWork.Save();
            }

            return RedirectToAction("Index", "Bio");
        }
    }
}
