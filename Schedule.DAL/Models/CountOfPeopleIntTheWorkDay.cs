using System;
using System.Collections.Generic;
using Schedule.DAL;

namespace Schedule
{
    public partial class CountOfPeopleIntTheWorkDay: DomainObject
    {
       // public int Id { get; set; }
        public int CountPeopleWork { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
