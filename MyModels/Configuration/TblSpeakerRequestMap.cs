using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblSpeakerRequestMap : EntityTypeConfiguration<TblSpeakerRequest>
    {
        public TblSpeakerRequestMap()
        {

            ToTable("iTblSpeakerRequest");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(a=> a.Adress).HasMaxLength(500).IsUnicode(true).IsVariableLength();
            Property(a=> a.ActivityType).IsRequired().HasMaxLength(500).IsUnicode(true).IsVariableLength();
            Property(a=> a.CompanyName).IsRequired().HasMaxLength(500).IsVariableLength();
            Property(a=> a.CooperationCode).HasMaxLength(100).IsVariableLength();
            Property(a=> a.Email).HasMaxLength(150).IsVariableLength();
            Property(a=> a.ExactSubject).HasMaxLength(250).IsVariableLength();
            Property(a=> a.IndicatorActivity).HasMaxLength(500).IsVariableLength();
            Property(a=> a.PhoneNumber).HasMaxLength(20).IsVariableLength();
            Property(a=> a.ResponsibleMobile).HasMaxLength(20).IsVariableLength();
            Property(a=> a.ResponsibleName).HasMaxLength(150).IsVariableLength();
            Property(a=> a.SiteUrl).HasMaxLength(250).IsVariableLength();
            Property(a => a.Description).HasMaxLength(1000).IsUnicode(true).IsVariableLength();

            HasRequired(c => c.TblUser)
                .WithMany(outer => outer.TblSpeakerRequests)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.TblCity)
                .WithMany(outer => outer.TblSpeakerRequests)
                .HasForeignKey(c => c.CityId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.TblSpeaker)
                .WithMany(outer => outer.TblSpeakerRequests)
                .HasForeignKey(c => c.SpeakerId)
                .WillCascadeOnDelete(false);

            //HasOptional(c => c.TblCategory)
            //    .WithMany(outer => outer.TblSpeakerRequests)
            //    .HasForeignKey(c => c.CategoryId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
