using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleNahui.DAL.Projection
{
    public class ScheduleProjectionForDg
    {
       
        [DisplayName("ФИО")]
        public string FIO { get; set; }

        [DisplayName("должность")]
        public string Dolj { get; set; }

        

        //public DateTime SumTime { get; set; }
        [DisplayName("Дата и время")]
        public string DateAndTime
        {
            get { return Date.ToString("d") + "\n" + StartTime.ToString(@"hh\:mm") + " - " + EndTime.ToString(@"hh\:mm"); }
        }
        //public string Datee { get; set; }
        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int EmployeeId { get; set; }
        public int Id { get; set; }

        

    }
}
