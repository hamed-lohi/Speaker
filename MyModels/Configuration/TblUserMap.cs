using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;
using MyModels.Models;

namespace MyModels.Configuration
{
    internal class TblUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public TblUserMap()
        {
            // Note: Attribute is better!
            //نام جدول چه باشد
            ToTable("iTblUser");//.Property(p => p.Id).HasColumnName("UserId");

            // Note: Attribute is better!
            //پرایمری نمودن آی دی
            //HasKey(c => c.Id);

            // Note: Attribute is better!
            //آی دی اتو اینکریمنت باشد
            //Property(current => current.Id)
            //    .HasDatabaseGeneratedOption
            //    (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            //Property(c => c.Id).HasColumnName("UserId").HasColumnOrder(0);
            //Property(c => c.RoleId).HasColumnOrder(1);
            Property(c => c.FullName).IsRequired().HasMaxLength(100).IsUnicode(true).IsVariableLength();
            //Property(c => c.UserName).HasColumnOrder(3).IsRequired().HasMaxLength(50).IsUnicode(true).IsVariableLength();
            //Property(c => c.Password).HasColumnOrder(5).IsRequired().HasMaxLength(40).IsUnicode(true).IsVariableLength();
            //Property(c => c.Tel).HasColumnOrder(7).IsRequired().HasMaxLength(12).IsVariableLength();
            //Property(c => c.Email).HasColumnOrder(9).IsRequired().HasMaxLength(40).IsVariableLength();
            //Property(c => c.Image).HasMaxLength(200).IsUnicode(true).IsVariableLength();


            //HasOptional(c => c.TblRole)
            //    .WithMany(country => country.TblUsers)
            //    .HasForeignKey(c => c.RoleId)
            //    .WillCascadeOnDelete(false);

            HasRequired(c => c.TblCity)
                .WithMany(outer => outer.TblSpeakers)
                .HasForeignKey(c => c.CityId)
                .WillCascadeOnDelete(false);

        }
    }
}
