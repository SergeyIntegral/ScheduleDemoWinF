using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL.Projection
{
    public class EmployeeProjection
    {
       // public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public int PositionId { get; set; }

        public int EmployeeId { get; set; }

        public string Position { get; set; }

        
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }

        public Nullable<System.TimeSpan> SumTime { get; set; }
        public List<DateTime[]> AllHolidays { get; set; }
        public DateTime LastMounthDateTime { get; set; }
        public List<DateTime> DatesOfMounth { get; set; }

    }
}
