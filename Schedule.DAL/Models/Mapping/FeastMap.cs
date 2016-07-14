using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Schedule
{
    public class FeastMap : EntityTypeConfiguration<Feast>
    {
        public FeastMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Feast");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Mounth).HasColumnName("Mounth");
            this.Property(t => t.DayOfMounth).HasColumnName("DayOfMounth");
        }
    }
}
