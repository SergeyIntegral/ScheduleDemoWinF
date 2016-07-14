using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL.Projection
{
    public class HolidayProjection
    {
        

        [DisplayName("ФИО Сотрудника")]
        public string FIO { get; set; }

        [DisplayName("Дата Начала")]
        public System.DateTime StartDate { get; set; }

        [DisplayName("Дата Конца")]
        public System.DateTime EndDate { get; set; }


        public int Id { get; set; }


    }
}
