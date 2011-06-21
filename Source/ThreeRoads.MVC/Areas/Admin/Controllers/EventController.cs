using System;
using System.Web.Mvc;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.SqlServerCe;
using ThreeRoads.MVC.Controllers;
using ThreeRoads.Data.Models;

namespace ThreeRoads.MVC.Areas.Admin.Controllers
{
    public class EventController : BaseController
    {
        private IEventRepository _eventRepository;
        private ICategoryRepository _categoryRepository;

        public EventController()
        {
            _eventRepository = new EventRepository(UnitOfWork);
            _categoryRepository = new CategoryRepository(UnitOfWork);
        }

        public EventController(IUnitOfWork unitOfWork, IEventRepository eventRepository, ICategoryRepository categoryRepository)
            : base(unitOfWork)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(_eventRepository.GetAll());
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var evt = _eventRepository.GetById(id);
            evt.Description = evt.Description.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
            ViewData["categories"] = new SelectList(_categoryRepository.FindByType("evt"), "ID", "Name", evt.Category.ID);

            return View(evt);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, Event evt, int categoryId)
        {
            var EventInDb = _eventRepository.GetById(id);

            EventInDb.Category = _categoryRepository.GetById(categoryId);

            if (TryUpdateModel(EventInDb))
            {
                UnitOfWork.Save();
                return RedirectToAction("Index", "Event");
            }

            return View(evt);
        }
        [Authorize]
        public ActionResult Create(string topic)
        {
            Event evt = new Event();
            evt.Topic = topic;
            evt.Start = DateTime.Now;
            evt.End = DateTime.Now.AddHours(1);

            ViewData["categories"] = new SelectList(_categoryRepository.FindByType("evt"), "ID", "Name", _categoryRepository.GetByIndex(0));

            return View(evt);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Event evt, int categoryId)
        {
            if (ModelState.IsValid)
            {
                evt.Category = _categoryRepository.GetById(categoryId);
                UpdateModel<Event>(evt);

                _eventRepository.Add(evt);
                UnitOfWork.Save();
                return RedirectToAction("Index", "Event"); // action = BoardAndStaff
            }

            return View(evt);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Event evt = _eventRepository.GetById(id);

            if (evt == null)
                return View("NotFound");
            else
                return View(evt);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            Event evt = _eventRepository.GetById(id);

            if (evt != null)
            {
                string topic = evt.Topic;

                _eventRepository.Remove(evt);
                UnitOfWork.Save();
            }

            return RedirectToAction("Index", "Event");
        }
    }
}
