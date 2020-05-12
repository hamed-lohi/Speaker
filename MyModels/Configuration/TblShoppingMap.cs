using System.Data.Entity.ModelConfiguration;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblShoppingMap : EntityTypeConfiguration<TblShopping>
    {
        public TblShoppingMap()
        {

            ToTable("iTblShopping");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);
            Property(c => c.Description).HasMaxLength(1500).IsUnicode(true).IsVariableLength();


           HasRequired(c => c.TblUser)
                .WithMany(user => user.TblShoppings)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

           //HasOptional(c => c.TblPost)
           //     .WithMany(user => user.TblShoppings)
           //     .HasForeignKey(c => c.PostId)
           //     .WillCascadeOnDelete(false);

           HasRequired(c => c.TblService)
                .WithMany(user => user.TblShoppings)
                .HasForeignKey(c => c.ServiceId)
                .WillCascadeOnDelete(false);
        }
    }
}
