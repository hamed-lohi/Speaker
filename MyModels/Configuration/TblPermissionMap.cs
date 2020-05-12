using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;


namespace MyModels.Configuration
{
    internal class TblPermissionMap : EntityTypeConfiguration<TblPermission>
    {
        public TblPermissionMap()
        {

            ToTable("iTblPermission");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasOptional(c => c.TblUser)
                .WithMany(outer => outer.TblPermissions)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(true);

        }
    }
}
