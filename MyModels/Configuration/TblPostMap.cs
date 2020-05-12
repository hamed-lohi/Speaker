using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblPostMap : EntityTypeConfiguration<TblPost>
    {
        public TblPostMap()
        {
            // Note: Attribute is better!
            //نام جدول چه باشد
            ToTable("iTblPost");

            // Note: Attribute is better!
            //پرایمری نمودن آی دی
            HasKey(c => c.Id);

            // Note: Attribute is better!
            //آی دی اتو اینکریمنت باشد
            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Title).IsRequired().HasMaxLength(200).IsUnicode(true).IsVariableLength();
            Property(c => c.Summary).IsRequired().HasMaxLength(7000).IsUnicode(true).IsVariableLength();
            Property(c => c.Text).IsRequired().HasMaxLength(10000).IsUnicode(true).IsVariableLength();

            HasRequired(c => c.TblUser)
                .WithMany(outer => outer.TblPosts)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            HasOptional(c => c.TblFileImage)
                .WithMany(outer => outer.TblPosts)
                .HasForeignKey(c => c.ImageFileId)
                .WillCascadeOnDelete(false);

        }
    }
}
