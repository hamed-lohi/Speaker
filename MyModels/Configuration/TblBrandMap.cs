using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblBrandMap : EntityTypeConfiguration<TblBrand>
    {
        public TblBrandMap()
        {

            ToTable("iTblBrand");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(0);
            Property(c => c.PId).HasColumnOrder(1);
            Property(c => c.SSModel).HasColumnOrder(2);
            Property(c => c.BrandName).HasColumnOrder(3).IsRequired().HasMaxLength(70).IsUnicode(true).IsVariableLength();
            //Property(c => c.CategoryId).HasColumnOrder(3);
            Property(c => c.Priority).HasColumnOrder(4);
            Property(c => c.IconUrl).HasColumnOrder(5).HasMaxLength(500).IsVariableLength();
            Property(c => c.State).HasColumnOrder(6);
            Property(c => c.LastUpdate).HasColumnOrder(20);
            Property(c => c.Description).HasColumnOrder(40).HasMaxLength(1000).IsUnicode(true).IsVariableLength();

            HasOptional(c => c.Parent)
                .WithMany(outer => outer.TblBrands)
                .HasForeignKey(c => c.PId)
                .WillCascadeOnDelete(false);

            //HasOptional(c => c.TblCategory)
            //    .WithMany(outer => outer.TblBrands)
            //    .HasForeignKey(c => c.CategoryId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
