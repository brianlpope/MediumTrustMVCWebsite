using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThreeRoads.Data.Contracts;
using ThreeRoads.MVC.Infrastructure;
using ThreeRoads.Data.Models;
using ThreeRoads.Data.SqlServerCe;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using ThreeRoads.MVC.Models;
using ThreeRoads.MVC.Services;

namespace ThreeRoads.MVC.Controllers
{
    public class CompositeController : BaseController
    {

        private IArticleRepository _articleRepository { get; set; }
        private IBioRepository _bioRepository;
        private IEventRepository _eventRepository;
        private IQARepository _qaRepository;
        private IDownloadRepository _downloadRepository;
        private ISearchRepository _searchRepository;

        public CompositeController()
        {
            _articleRepository = new ArticleRepository(UnitOfWork);
            _bioRepository = new BioRepository(UnitOfWork);
            _eventRepository = new EventRepository(UnitOfWork);
            _qaRepository = new QARepository(UnitOfWork);
            _downloadRepository = new DownloadRepository(UnitOfWork);
            _searchRepository = new SearchRepository(UnitOfWork);
        }

        public CompositeController(IUnitOfWork unitOfWork, IArticleRepository articleRepository, IBioRepository bioRepository,
            IEventRepository eventRepository, IQARepository qaRepository, IDownloadRepository downloadRepository, ISearchRepository searchRepository)
            : base(unitOfWork)
        {
            _articleRepository = articleRepository;
            _bioRepository = bioRepository;
            _eventRepository = eventRepository;
            _qaRepository = qaRepository;
            _downloadRepository = downloadRepository;
            _searchRepository = searchRepository;
        }
        [HandleError]
        public PartialViewResult Article(string topic)
        {
            var result = _articleRepository.FindByTopic(topic);
            //result.Body = result.Body.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
            return PartialView("BasicArticle", result);
        }
        [HandleError]
        public ActionResult Staff()
        {
            ViewBag.Title = "Staff";
            return View("Staff", _bioRepository.GetStaff());
        }
        [HandleError]
        public ActionResult Donate()
        {
            return View(_articleRepository.FindByTopic("Donate"));
        }
        [HandleError]
        public ActionResult Board()
        {
            ViewBag.Title = "Board Members";
            return View("Staff", _bioRepository.GetBoard());
        }
        [HandleError]
        public ActionResult Events()
        {
            return View(_eventRepository.GetAll());
        }
        [HandleError]
        public ActionResult Questions()
        {
            return View("FrequentQuestions", _qaRepository.GetAll());
        }
        [HandleError]
        public ActionResult Welcome()
        {
            var result = _articleRepository.FindByTopic("welcome");
            return View(result);
        }
        [HandleError]
        public JsonResult CalendarEvents(long start, long end)
        {
            DateTime startDate = Helper.ConvertDateFromUnix(start);
            DateTime endDate = Helper.ConvertDateFromUnix(end);

            IEnumerable<Event> events = _eventRepository.GetAll().Where(e => e.Start >= startDate && e.End <= endDate).AsEnumerable();

            var eventList = from e in events
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.Start.ToString("s"),
                                end = e.End.ToString("s"),
                                allDay = e.AllDay
                            };

            return Json(eventList.ToArray(), JsonRequestBehavior.AllowGet);

        }
        [HandleError]
        public ActionResult Location()
        {
            var result = _articleRepository.FindByTopic("Location");
            return View(result);
        }
        [HandleError]
        public ActionResult Contact()
        {
            var result = _articleRepository.FindByTopic("Contact");
            return View(result);
        }
        //public ActionResult Coalition()
        //{
        //    return View("PhoenixCoalition");
        //}
        [HandleError]
        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            string message = "There are a few errors";
            try
            {
                if (ModelState.IsValid)
                {
                    string subject = System.Configuration.ConfigurationManager.AppSettings["EmailSubject"];
                    string toAddress = System.Configuration.ConfigurationManager.AppSettings["EmailToAddress"];
                    string toName = System.Configuration.ConfigurationManager.AppSettings["EmailToName"];
                    MailService.SendEmailMessage(model.Name, model.Email, model.Comment, subject, toAddress, toName);
                    message = "Thank you for your comments.";
                }

                if (Request.IsAjaxRequest())
                {
                    return new JsonResult { ContentEncoding = Encoding.UTF8, Data = new { success = true, message = message } };
                }

                TempData["Message"] = message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return View();
        }
        [HandleError]
        public ActionResult ERB()
        {
            string documentPath = ConfigurationManager.AppSettings["DocumentPath"];
            documentPath = documentPath.Replace("~/", "");
            documentPath = string.Concat(documentPath, "/erb");

            IEnumerable<Download> downloads = _downloadRepository.GetPathedList(documentPath).Where(i=>i.Topic=="ERB");
 
            ViewBag.ErbArticle = _articleRepository.FindByTopic("ERB");
            return View(downloads);
        }
        [HandleError]
        public ActionResult Downloads()
        {
            string documentPath = ConfigurationManager.AppSettings["DocumentPath"];
            documentPath = documentPath.Replace("~/", "");

            IEnumerable<Download> downloads = _downloadRepository.GetPathedList(documentPath).Where(i => i.Topic == "Download");

            return View(downloads);
        }
        [HandleError]
        public ActionResult Calendar()
        {
            return View();
        }
        [HandleError]
        public ActionResult Video()
        {
            return View();
        }
        [HandleError]
        public ActionResult SearchResults(string query)
        {
            List<SearchResult> searchResults = _searchRepository.FindMatchingContent(query);
            List<SearchResult> items = SearchService.FormatSearchResults(query, searchResults);

            return View(items);
        }
        private SearchServices _searchService;
        public SearchServices SearchService
        {
            get
            {
                if (_searchService == null)
                    _searchService = new SearchServices();

                return _searchService;
            }
            set
            {
                _searchService = value;
            }
        }
        private EmailServices _mailService;
        public EmailServices MailService
        {
            get
            {
                if (_mailService == null)
                    _mailService = new EmailServices();

                return _mailService;
            }
            set
            {
                _mailService = value;
            }
        }
    }
}
