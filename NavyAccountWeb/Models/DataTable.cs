

using System.Collections.Generic;

namespace NavyAccountWeb.Models
{
    public class DataTable<T>
    {
        public DataTable(List<T> t)
        {
            aaData = t;
        }

        public string sEcho { get; set; }

        public int iDisplayStart { get; set; }

        public int iDisplayLength { get; set; }

        public int iTotalRecords { get; set; }

        public int iTotalDisplayRecords { get; set; }

        public string status { get; set; }

        public string errormessage { get; set; }

        public List<T> aaData;
    }
}
