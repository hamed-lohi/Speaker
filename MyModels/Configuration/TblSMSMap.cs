using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblSMSMap : EntityTypeConfiguration<TblSMS>
    {
        public TblSMSMap()
        {

            ToTable("iTblSMS");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(0);
            Property(c => c.Code).HasColumnOrder(3);
            Property(c => c.Tel).HasColumnOrder(4).IsRequired().HasMaxLength(15).IsVariableLength();
            Property(c => c.UserId).HasColumnOrder(5);
            Property(c => c.SendTime).HasColumnOrder(8);
            Property(c => c.State).HasColumnOrder(10);


            HasRequired(c => c.TblUser)
                .WithMany(country => country.TblSMSs)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

        }
    }
}
