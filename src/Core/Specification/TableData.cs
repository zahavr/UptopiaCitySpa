using System.Collections.Generic;

namespace Core.Specification
{
	public class TableData<T>
    {
        public IReadOnlyList<T> Data { get; set; }
        public int Count { get; set; }
    }
}
