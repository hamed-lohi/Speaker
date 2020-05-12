using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblFileMap : EntityTypeConfiguration<TblFile>
    {
        public TblFileMap()
        {

            ToTable("iTblFile");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(a=> a.FileName).IsRequired().HasMaxLength(400).IsUnicode(true).IsVariableLength();
            Property(a=> a.Extension).HasMaxLength(10).IsUnicode(true).IsVariableLength();
            Property(a=> a.MimeType).HasMaxLength(150).IsVariableLength();
            Property(a=> a.FileUrl).HasMaxLength(500).IsVariableLength();
            Property(a=> a.FilePath).HasMaxLength(500).IsVariableLength();
            Property(a => a.Description).HasMaxLength(1000).IsUnicode(true).IsVariableLength();

            HasRequired(c => c.TblUser)
                .WithMany(outer => outer.TblFiles)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

        }
    }
}
