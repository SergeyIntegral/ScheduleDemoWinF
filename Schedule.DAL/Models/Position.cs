using System;
using System.Collections.Generic;
using Schedule.DAL;

namespace Schedule
{
    public partial class Position:DomainObject
    {
        public Position()
        {
            this.CountOfPeopleIntTheWorkDays = new List<CountOfPeopleIntTheWorkDay>();
            this.Employees = new List<Employee>();
            this.Weekends = new List<Weekend>();
        }

       // public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<CountOfPeopleIntTheWorkDay> CountOfPeopleIntTheWorkDays { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Weekend> Weekends { get; set; }
    }
}
