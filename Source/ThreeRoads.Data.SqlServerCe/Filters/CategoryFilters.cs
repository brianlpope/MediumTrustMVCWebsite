using System.Linq;
using ThreeRoads.Data.Models;

namespace ThreeRoads.Data.SqlServerCe.Filters
{
    public static class CategoryFilters
    {
        /// <summary>
        /// Filters an IList of Category and returns a Category by name
        /// </summary>
        public static IQueryable<Category> WithGroupName(this IQueryable<Category> qry, string groupName)
        {
            return qry.Where(c=>c.Type.ToLower() == groupName);
        }
    }
}
