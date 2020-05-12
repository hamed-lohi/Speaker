using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblConstMap : EntityTypeConfiguration<TblConst>
    {
        public TblConstMap()
        {

            ToTable("iTblConst");

            HasKey(c => c.Id);
            
            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.ConstName).IsRequired().HasMaxLength(70).IsUnicode(true).IsVariableLength();
            Property(c => c.Description).HasMaxLength(1000).IsUnicode(true).IsVariableLength();


            HasOptional(c => c.Parent)
                .WithMany(c => c.TblConsts)
                .HasForeignKey(c => c.PId)
                .WillCascadeOnDelete(false);

            HasOptional(c => c.TblFileImage)
                .WithMany(outer => outer.TblConsts)
                .HasForeignKey(c => c.ImageFileId)
                .WillCascadeOnDelete(false);
        }
    }
}
