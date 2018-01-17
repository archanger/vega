using System.Collections.Generic;

namespace vega.Controllers.Resources
{
    public class QueryResultResource<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> items { get; set; }
    }
}