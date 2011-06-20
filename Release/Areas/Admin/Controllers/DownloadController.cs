using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThreeRoads.MVC.Controllers;
using ThreeRoads.Data.Models;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.SqlServerCe;

namespace ThreeRoads.MVC.Areas.Admin.Controllers
{
    public class DownloadController : BaseController
    {
        private string _documentPath = ConfigurationManager.AppSettings["DocumentPath"];
        private IDownloadRepository _downloadRepository;

        public DownloadController()
        {
            _downloadRepository = new DownloadRepository(UnitOfWork);
        }

        public DownloadController(IUnitOfWork unitOfWork, IDownloadRepository formRepository)
            : base(unitOfWork)
        {
            _downloadRepository = formRepository;
        }

        public ActionResult Index()
        {
            IEnumerable<Download> downloads = _downloadRepository.GetAll().AsEnumerable();
            foreach (var download in downloads)
            {
                download.FileName = string.Concat(_documentPath, "\\", download.FileName);
            }
            return View(downloads);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Download download = _downloadRepository.GetById(id);

            return View(download);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, Download download, HttpPostedFileBase file)
        {
            var formInDb = _downloadRepository.GetById(id);


            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath(_documentPath), fileName));
                formInDb.FileName = fileName;
            }

            if (TryUpdateModel(formInDb))
            {
                UnitOfWork.Save();
                return RedirectToAction("Index", "Download");
            }

            return View(download);
        }

        [Authorize]
        public ActionResult Create(string topic)
        {
            Download download = new Download();
            download.Topic = topic;

            return View(download);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Download download, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath(_documentPath), fileName));
                download.FileName = fileName;
                UpdateModel<Download>(download);

                _downloadRepository.Add(download);
                UnitOfWork.Save();
                return RedirectToAction("Index", "Download");
            }

            return View(download);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Download download = _downloadRepository.GetById(id);

            if (download == null)
                return View("NotFound");
            else
                return View(download);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            Download download = _downloadRepository.GetById(id);

            if (download != null)
            {
                _downloadRepository.Remove(download);
                UnitOfWork.Save();
            }

            return RedirectToAction("Index", "Download");
        }
    }
}
