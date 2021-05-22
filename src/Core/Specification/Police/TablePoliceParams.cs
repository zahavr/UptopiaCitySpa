using System.Collections.Generic;

namespace Core.Specification
{
	public class TablePoliceParams
    {
        public List<TableFilterItem> Filters { get; set; }
        public int First { get; set; }
        public string GlobalFilter { get; set; }
        public int Rows { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; }
    }
}
