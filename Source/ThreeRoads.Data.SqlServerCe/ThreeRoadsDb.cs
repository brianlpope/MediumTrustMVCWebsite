using System.Data.Entity;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ThreeRoads.Data.SqlServerCe
{
    public class ThreeRoadsDb : DbContext, IUnitOfWork
    {

        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Event> Events { get; set; }
        public IDbSet<Bio> Bios { get; set; }
        public IDbSet<Resource> Resources { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Download> Forms { get; set; }
        public IDbSet<QA> QA { get; set; }
        public IDbSet<Search> Search { get; set; }
        public IDbSet<Gallery> Galleries { get; set; }

        public void Save()
        {
            SaveChanges();
        }
    }
}
