using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.Contracts
{
    public interface ISearchRepository : IRepository<Search>
    {
        List<SearchResult> FindMatchingContent(string query);
    }
}
