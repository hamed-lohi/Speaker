using System.Data.Entity.ModelConfiguration;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblChargeMap : EntityTypeConfiguration<TblCharge>
    {
        public TblChargeMap()
        {

            ToTable("iTblCharge");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);
            Property(c => c.UserId).HasColumnOrder(3);
            Property(c => c.ChargeOptionId).HasColumnOrder(5);
            Property(c => c.ChargeTime).HasColumnOrder(10);
            Property(c => c.Amount).HasColumnOrder(20);
            Property(c => c.Code).HasColumnOrder(25).HasMaxLength(40).IsVariableLength();
            Property(c => c.State).HasColumnOrder(30);


           HasRequired(c => c.TblUser)
                .WithMany(user => user.TblCharges)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

           //HasOptional(c => c.ChargeOption)
           //     .WithMany(user => user.TblTransactions)
           //     .HasForeignKey(c => c.ChargeId)
           //     .WillCascadeOnDelete(false);
        }
    }
}
