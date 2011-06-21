using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeRoads.Data.Models;
using ThreeRoads.Data.Contracts;
using System.ComponentModel.DataAnnotations;

namespace ThreeRoads.Data.SqlServerCe
{
    [Table("QA")]
    public class QARepository : Repository<QA>, IQARepository
    {
        public QARepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
