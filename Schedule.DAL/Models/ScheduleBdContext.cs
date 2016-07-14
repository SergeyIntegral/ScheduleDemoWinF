using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace Schedule
{
    public partial class ScheduleBdContext : DbContext
    {
        static ScheduleBdContext()
        {
            Database.SetInitializer<ScheduleBdContext>(null);
        }

        public ScheduleBdContext()
            : base("Name=ScheduleBdContext")
        {
        }

        public DbSet<CountOfPeopleIntTheWorkDay> CountOfPeopleIntTheWorkDays { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feast> Feasts { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Weekend> Weekends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountOfPeopleIntTheWorkDayMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new FeastMap());
            modelBuilder.Configurations.Add(new HolidayMap());
            modelBuilder.Configurations.Add(new PositionMap());
            modelBuilder.Configurations.Add(new ScheduleMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new WeekendMap());
        }
    }
}
