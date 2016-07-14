using System;
using System.Collections.Generic;
using Schedule.DAL;

namespace Schedule
{
    public partial class Holiday:DomainObject
    {
        //public int Id { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
