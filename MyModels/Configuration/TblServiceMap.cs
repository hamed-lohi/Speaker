using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblServiceMap : EntityTypeConfiguration<TblService>
    {
        public TblServiceMap()
        {

            ToTable("iTblService");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);
            Property(c => c.ServiceName).HasColumnOrder(5).IsRequired().HasMaxLength(150).IsUnicode(true).IsVariableLength();
            Property(c => c.Cost).HasColumnOrder(8);
            Property(c => c.IconUrl).HasColumnOrder(9).IsRequired().HasMaxLength(200).IsVariableLength();
            Property(c => c.LastUpdate).HasColumnOrder(13);
            Property(c => c.Description).HasColumnOrder(15).HasMaxLength(1000).IsUnicode(true).IsVariableLength();
            Property(c => c.HelpImageUrl).HasColumnOrder(20);
            Property(c => c.Time).HasColumnOrder(25);
            Property(c => c.State).HasColumnOrder(30);
        }
    }
}
