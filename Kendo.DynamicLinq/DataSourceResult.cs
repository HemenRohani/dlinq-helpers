using System;
using System.Linq;
using System.Collections;

namespace Kendo.DynamicLinq
{

    public class DataSourceResult
    {
        public IEnumerable Data { get; set; }
        public IEnumerable Group { get; set; }
        public int Total { get; set; }
        public object Aggregates { get; set; }
        public object Errors { get; set; }
    }
}
