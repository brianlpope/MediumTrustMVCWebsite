using System.Collections.Generic;
using System.Linq;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.SqlServerCe
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Article FindByTopic(string topic)
        {
            return _unitOfWork.Articles.Where(a => a.Topic == topic).FirstOrDefault();

        }


    }
}
