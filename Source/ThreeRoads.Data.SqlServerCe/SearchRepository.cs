using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.SqlServerCe
{
    public class SearchRepository : Repository<Search>, ISearchRepository
    {
        private IResourceRepository _resourceRepository;
        private IBioRepository _bioRepository;
        private IArticleRepository _articleRepository;
        private IEventRepository _eventRepository;

        public SearchRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _resourceRepository = new ResourceRepository(unitOfWork);
            _bioRepository = new BioRepository(unitOfWork);
            _articleRepository = new ArticleRepository(unitOfWork);
            _eventRepository = new EventRepository(unitOfWork);
        }

        public SearchRepository(IUnitOfWork unitOfWork, IResourceRepository ResourceRepository, IBioRepository bioRepository, IArticleRepository articleRepository, IEventRepository eventRepository)
            : base(unitOfWork)
        {
            _resourceRepository = ResourceRepository;
            _bioRepository = bioRepository;
            _articleRepository = articleRepository;
            _eventRepository = eventRepository;
        }
        public List<SearchResult> FindMatchingContent(string query)
        {
            // do search
            // articles
            List<SearchResult> searchResults = new List<SearchResult>();
            searchResults = _articleRepository.GetAll().Where(a => a.Body.Contains(query))
                .Select(b => new SearchResult { Url=b.Url, Name = b.Title, PublishedOn = b.PublishedOn, ResultType = "Article", Value = b.Body }).ToList();

            searchResults.AddRange(_bioRepository.GetAll().Where(a => a.Name.Contains(query))
                .Select(b => new SearchResult { Name = b.Name, PublishedOn = b.PublishedOn, ResultType = "Bio", Url = b.Url, Value = b.Name }));

            searchResults.AddRange(_bioRepository.GetAll().Where(a => a.Position.Contains(query))
                .Select(b => new SearchResult { Name = b.Name, PublishedOn = b.PublishedOn, ResultType = "Bio", Url = b.Url, Value = b.Position }));

            searchResults.AddRange(_bioRepository.GetAll().Where(a => a.Description.Contains(query))
                .Select(b => new SearchResult { Name = b.Name, PublishedOn = b.PublishedOn, ResultType = "Bio", Url = b.Url, Value = b.Description }));

            //searchResults.AddRange(_eventRepository.GetAll().Where(a => a.Description.Contains(query))
            //    .Select(b => new SearchResult { Author = "", Name = b.Title, PublishedOn = b.PublishedOn, ResultType = "Event", Url = string.Concat("~/", b.DisplayArea, "/", b.Topic), Value = b.Description }));
            
            SaveSearch(query, searchResults);

            return searchResults;

        }

        private void SaveSearch(string query, List<SearchResult> searchResults)
        {
            Search search = new Search();
            search.PagesMatched = searchResults.Count;
            search.SearchDate = DateTime.Today;
            search.SearchTerm = query;

            _unitOfWork.Search.Add(search);
            _unitOfWork.Save();
        }
    }
}
