using System.Collections.Generic;

namespace Models.Shared
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }

        public int Count { get; set; }
    }
}
