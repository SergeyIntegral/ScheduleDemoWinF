using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Schedule.DAL;

namespace Schedule
{
    public partial class Employee : DomainObject
    {
        public Employee()
        {
            this.Holidays = new List<Holiday>();
            this.Schedules = new List<Schedule>();
        }

        // public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Holiday> Holidays { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }

        [NotMapped]

        public string FIO
        {
            get
            {
                return Name + " " + LastName + " " + MiddleName;
            }
        }
    }
}
