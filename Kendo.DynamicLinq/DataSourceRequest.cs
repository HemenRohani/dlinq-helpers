using System.Collections.Generic;

namespace Kendo.DynamicLinq
{
    public class DataSourceRequest
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public IEnumerable<Sort> Sort { get; set; }
        public Filter Filter { get; set; }
        public IEnumerable<Aggregator> Aggregate { get; set; }
        public IEnumerable<Group> Group { get; set; }
    }
}
