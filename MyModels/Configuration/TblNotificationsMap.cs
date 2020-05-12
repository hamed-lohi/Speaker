using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblNotificationsMap : EntityTypeConfiguration<TblNotifications>
    {
        public TblNotificationsMap()
        {

            ToTable("iTblNotifications");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(0);
            Property(c => c.UserId).HasColumnOrder(3);
            //Property(c => c.PostId).HasColumnOrder(4);
            Property(c => c.Title).HasColumnOrder(5).IsRequired().HasMaxLength(150).IsUnicode(true).IsVariableLength();
            Property(c => c.Description).HasColumnOrder(7).HasMaxLength(1500).IsUnicode(true).IsVariableLength();
            Property(c => c.Date).HasColumnOrder(9);
            Property(c => c.State).HasColumnOrder(11);
            Property(c => c.LastUpdate).HasColumnOrder(13);


            HasOptional(c => c.TblUser)
                .WithMany(user => user.TblNotificationss)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);
            
            //HasOptional(c => c.TblPost)
            //    .WithMany(post => post.TblNotificationss)
            //    .HasForeignKey(c => c.PostId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
