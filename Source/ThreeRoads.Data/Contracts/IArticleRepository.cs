using System.Collections.Generic;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.Contracts
{
    public interface IArticleRepository : IRepository<Article>
    {
        Article FindByTopic(string topic);
    }
}
