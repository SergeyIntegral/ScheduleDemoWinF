using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Schedule
{
    public class WeekendMap : EntityTypeConfiguration<Weekend>
    {
        public WeekendMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Weekend");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HolidayForPosition).HasColumnName("HolidayForPosition");
            this.Property(t => t.PositionId).HasColumnName("PositionId");

            // Relationships
            this.HasRequired(t => t.Position)
                .WithMany(t => t.Weekends)
                .HasForeignKey(d => d.PositionId);

        }
    }
}
