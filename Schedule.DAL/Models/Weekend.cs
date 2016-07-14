using System;
using System.Collections.Generic;
using Schedule.DAL;

namespace Schedule
{
    public partial class Weekend:DomainObject
    {
       // public int Id { get; set; }
        public int HolidayForPosition { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
