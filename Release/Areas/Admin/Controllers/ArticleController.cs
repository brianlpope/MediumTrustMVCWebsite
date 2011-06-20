using System;
using System.Linq;
using System.Web.Mvc;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.SqlServerCe;
using ThreeRoads.MVC.Controllers;
using ThreeRoads.Data.Models;

namespace ThreeRoads.MVC.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        IArticleRepository _articleRepository;

        public ArticleController(IUnitOfWork unitOfWork, IArticleRepository articleRepository) : base(unitOfWork)
        {
            _articleRepository = articleRepository;
        }

        public ArticleController()
        {
            _articleRepository = new ArticleRepository(UnitOfWork);
        }
        public ActionResult Index()
        {
            return View(_articleRepository.GetAll().OrderBy(a => a.Topic));
        }

        public ActionResult Detail(string topic)
        {
            var result = _articleRepository.FindByTopic(topic);
            result.Body = result.Body.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
            return View(topic, result);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var result = _articleRepository.GetById(id);
            result.Body = string.IsNullOrEmpty(result.Body) ? "" :
                result.Body.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");

            return View(result);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, Article article)
        {
            var articleInDb = _articleRepository.GetById(id);
            articleInDb.PublishedOn = DateTime.Now;

            articleInDb.Body = string.IsNullOrEmpty(article.Body) ? "" :
                article.Body.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");

            if (TryUpdateModel(articleInDb))
            {
                UnitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(article);
        }
        [Authorize]
        public ActionResult Create(string topic)
        {
            Article article = new Article();
            article.Topic = topic;
            return View(article);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.PublishedOn = DateTime.Now;
                UpdateModel<Article>(article);

                _articleRepository.Add(article);
                UnitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(article);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var article = _articleRepository.GetById(id);
            if (article == null)
                return View("NotFound");
            else
                return View(article);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, Article article)
        {
            var articleInDb = _articleRepository.GetById(id);
            if (articleInDb != null)
            {
                _articleRepository.Remove(articleInDb);
                UnitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
