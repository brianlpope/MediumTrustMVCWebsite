using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeRoads.Data.Contracts
{
    interface ISearchable
    {
        string ResultType { get; set; }
        string Value { get; set; }
        string Name { get; set; }
        DateTime PublishedOn { get; set; }
        string Author { get; set; }
        string Url { get; set; }
        int MatchCount { get; set; }
    }
}
