using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeRoads.Data.Contracts
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
