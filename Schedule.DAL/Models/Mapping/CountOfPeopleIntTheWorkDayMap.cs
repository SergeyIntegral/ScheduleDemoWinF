using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Schedule
{
    public class CountOfPeopleIntTheWorkDayMap : EntityTypeConfiguration<CountOfPeopleIntTheWorkDay>
    {
        public CountOfPeopleIntTheWorkDayMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("CountOfPeopleIntTheWorkDay");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CountPeopleWork).HasColumnName("CountPeopleWork");
            this.Property(t => t.PositionId).HasColumnName("PositionId");

            // Relationships
            this.HasRequired(t => t.Position)
                .WithMany(t => t.CountOfPeopleIntTheWorkDays)
                .HasForeignKey(d => d.PositionId);

        }
    }
}
