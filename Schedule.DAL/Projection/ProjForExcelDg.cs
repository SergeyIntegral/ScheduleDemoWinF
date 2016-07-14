using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleNahui.DAL.Projection
{
    public class ProjForExcelDg
    {
        [DisplayName("ФИО")]
        public string FIO { get; set; }

        [DisplayName("должность")]
        public string Dolj { get; set; }

        public Dictionary<DateTime, string> ScheduleTable;

        public ProjForExcelDg()
        {
            ScheduleTable = new Dictionary<DateTime, string>();
        }
    }
}
