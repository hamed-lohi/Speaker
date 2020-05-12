using System.Data.Entity.ModelConfiguration;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblStatisticMap : EntityTypeConfiguration<TblStatistic>
    {
        public TblStatisticMap()
        {

            ToTable("iTblStatistic");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);
            Property(c => c.IMEI).HasColumnOrder(2);
            Property(c => c.Brand).HasColumnOrder(3).HasMaxLength(60).IsUnicode(true).IsVariableLength();
            Property(c => c.Model).HasColumnOrder(5).HasMaxLength(80).IsUnicode(true).IsVariableLength();
            Property(c => c.AndroidVersion).HasColumnOrder(6);
            Property(c => c.APILevel).HasColumnOrder(10);
            Property(c => c.Date).HasColumnOrder(17);
            Property(c => c.State).HasColumnOrder(20);
            
        }
    }
}
