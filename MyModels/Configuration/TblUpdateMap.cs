using System.Data.Entity.ModelConfiguration;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblUpdateMap : EntityTypeConfiguration<TblUpdate>
    {
        public TblUpdateMap()
        {

            ToTable("iTblUpdate");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);
            Property(c => c.VersionName).HasColumnOrder(3).IsRequired().HasMaxLength(100).IsUnicode(true).IsVariableLength();
            Property(c => c.VersionCode).HasColumnOrder(6);
            Property(c => c.ChangeLog).HasColumnOrder(10).HasMaxLength(1000).IsUnicode(true).IsVariableLength();
            Property(c => c.IsCritical).HasColumnOrder(15);
            Property(c => c.Date).HasColumnOrder(17);
            Property(c => c.State).HasColumnOrder(20);
            
        }
    }
}
