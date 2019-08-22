using System.Collections.Generic;

namespace HGP.Staff.Models
{
    public class TableModel
    {
        public List<string> Org { get; set; } = new List<string>();
        public List<List<string>> RowsData { get; set; } = new List<List<string>>();
    }
}
