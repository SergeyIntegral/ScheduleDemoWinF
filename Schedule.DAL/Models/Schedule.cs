using System;
using System.Collections.Generic;
using Schedule.DAL;

namespace Schedule
{
    public partial class Schedule:DomainObject
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<System.TimeSpan> SumTime { get; set; }
        public int EmployeeId { get; set; }

        public int? Summary { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
