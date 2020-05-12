using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblPostDeleteReasonMap : EntityTypeConfiguration<TblPostDeleteReason>
    {
        public TblPostDeleteReasonMap()
        {

            ToTable("iTblPostDeleteReason");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);
            Property(c => c.Description).HasColumnOrder(9).HasMaxLength(1000).IsUnicode(true).IsVariableLength();


            HasRequired(c => c.TblUser)
                .WithMany(country => country.TblPostDeleteReasons)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);
            
            //HasRequired(c => c.TblPost)
            //    .WithMany(country => country.TblPostDeleteReasons)
            //    .HasForeignKey(c => c.PostId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
