using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblSpeechFieldMap : EntityTypeConfiguration<TblSpeechField>
    {
        public TblSpeechFieldMap()
        {

            ToTable("iTblSpeechField");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasRequired(c => c.TblSpeaker)
                .WithMany(outer => outer.TblSpeechFields)
                .HasForeignKey(c => c.SpeakerId)
                .WillCascadeOnDelete(true);

            //HasOptional(c => c.TblCategory)
            //    .WithMany(outer => outer.TblSpeechFields)
            //    .HasForeignKey(c => c.CategoryId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
