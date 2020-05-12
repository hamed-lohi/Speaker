using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;


namespace MyModels.Configuration
{
    internal class TblSpeakerMap : EntityTypeConfiguration<TblSpeaker>
    {
        public TblSpeakerMap()
        {

            ToTable("iTblSpeaker");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(a=> a.FName).IsRequired().HasMaxLength(80).IsUnicode(true).IsVariableLength();
            Property(a=> a.LName).IsRequired().HasMaxLength(100).IsUnicode(true).IsVariableLength();
            Property(a=> a.MobileNumber).HasMaxLength(15).IsVariableLength();
            Property(a=> a.GroupName).HasMaxLength(500).IsUnicode(true).IsVariableLength();
            Property(a=> a.University).HasMaxLength(500).IsUnicode(true).IsVariableLength();
            Property(a=> a.Major).HasMaxLength(500).IsUnicode(true).IsVariableLength();
            Property(a=> a.Grade).HasMaxLength(500).IsUnicode(true).IsVariableLength();
            Property(a => a.Description).HasMaxLength(1000).IsUnicode(true).IsVariableLength();

            Property(a => a.ActivityDescription).HasMaxLength(1000000).IsUnicode(true).IsVariableLength();
            Property(a => a.EducationDescription).HasMaxLength(1000000).IsUnicode(true).IsVariableLength();
            Property(a => a.TeachingDescription).HasMaxLength(1000000).IsUnicode(true).IsVariableLength();
            Property(a => a.RecordsDescription).HasMaxLength(1000000).IsUnicode(true).IsVariableLength();
            Property(a => a.ResearchDescription).HasMaxLength(1000000).IsUnicode(true).IsVariableLength();
            Property(a => a.MasterDescription).HasMaxLength(1000000).IsUnicode(true).IsVariableLength();

            HasRequired(c => c.TblUser)
                .WithMany(outer => outer.TblSpeakers)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.TblCity)
                .WithMany(outer => outer.TblSpeakers)
                .HasForeignKey(c => c.CityId)
                .WillCascadeOnDelete(false);

            HasOptional(c => c.TblFileImage)
                .WithMany(outer => outer.TblImageSpeakers)
                .HasForeignKey(c => c.ImageFileId)
                .WillCascadeOnDelete(false);

            HasOptional(c => c.TblFileResume)
                .WithMany(outer => outer.TblResumeSpeakers)
                .HasForeignKey(c => c.ResumeFileId)
                .WillCascadeOnDelete(false);
        }
    }
}
