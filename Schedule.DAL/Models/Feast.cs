using System;
using System.Collections.Generic;
using Schedule.DAL;

namespace Schedule
{
    public partial class Feast:DomainObject
    {
      //  public int Id { get; set; }
        public int Mounth { get; set; }
        public int DayOfMounth { get; set; }
    }
}
