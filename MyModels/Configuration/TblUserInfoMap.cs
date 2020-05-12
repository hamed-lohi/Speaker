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
    internal class TblUserInfoMap : EntityTypeConfiguration<TblUserInfo>
    {
        public TblUserInfoMap()
        {
            // Note: Attribute is better!
            //نام جدول چه باشد
            ToTable("iTblUserInfo");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                    (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            //Property(c => c.FName).HasMaxLength(80).IsUnicode(true).IsVariableLength();
            //Property(c => c.LName).HasMaxLength(100).IsUnicode(true).IsVariableLength();
            Property(c => c.CompanyName).HasMaxLength(500).IsUnicode(true).IsVariableLength();
            Property(c => c.ActivityType).HasMaxLength(500).IsUnicode(true).IsVariableLength();
            Property(c => c.Adress).HasMaxLength(1000).IsUnicode(true).IsVariableLength();
            Property(c => c.ResponsibleName).HasMaxLength(200).IsUnicode(true).IsVariableLength();
            Property(a => a.ResponsibleMobile).HasMaxLength(20).IsVariableLength();
            Property(c => c.PhoneNumber).HasMaxLength(15).IsVariableLength();
            Property(c => c.SiteUrl).HasMaxLength(200).IsUnicode(true).IsVariableLength();


            HasRequired(c => c.TblUser)
                .WithMany(outer => outer.TblUserInfos)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            HasOptional(c => c.TblCity)
                .WithMany(outer => outer.TblUserInfos)
                .HasForeignKey(c => c.CityId)
                .WillCascadeOnDelete(false);

            HasOptional(c => c.TblFileImage)
                .WithMany(outer => outer.TblUserInfos)
                .HasForeignKey(c => c.ImageFileId)
                .WillCascadeOnDelete(false);

        }
    }
}
