using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThreeRoads.MVC.Controllers;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.SqlServerCe;
using ThreeRoads.Data.Models;

namespace ThreeRoads.MVC.Areas.Admin.Controllers
{
    public class QAController : BaseController
    {
        IQARepository _qaRepository;

        public QAController(IUnitOfWork unitOfWork, IQARepository qaRepository) : base(unitOfWork)
        {
            _qaRepository = qaRepository;
        }

        public QAController()
        {
            _qaRepository = new QARepository(UnitOfWork);
        }
        [Authorize]
        public ActionResult Index()
        {
            return View(_qaRepository.GetAll().OrderBy(a => a.Question));
        }
        [Authorize]
        public ActionResult Detail(int id)
        {
            var result = _qaRepository.GetById(id);
            return View(result);
        }

        [Authorize]
        public ActionResult Create()
        {
            QA model = new QA();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(QA model)
        {
            if (ModelState.IsValid)
            {
                _qaRepository.Add(model);
                UnitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var result = _qaRepository.GetById(id);
            result.Answer = result.Answer.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
            return View(result);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, QA model)
        {
            var qaInDb = _qaRepository.GetById(id);
            if (TryUpdateModel(qaInDb))
            {
                UnitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var qa = _qaRepository.GetById(id);
            if (qa == null)
                return View("NotFound");
            else
                return View(qa);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, QA model)
        {
            var modelInDb = _qaRepository.GetById(id);
            if (modelInDb != null)
            {
                _qaRepository.Remove(modelInDb);
                UnitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
