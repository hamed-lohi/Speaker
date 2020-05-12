using System.Data.Entity.ModelConfiguration;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblTicketMap : EntityTypeConfiguration<TblTicket>
    {
        public TblTicketMap()
        {

            ToTable("iTblTicket");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);
            Property(c => c.UserId).HasColumnOrder(3);
            //Property(c => c.SSDepartment).HasColumnOrder(4);
            Property(c => c.SSSubject).HasColumnOrder(6); 
            Property(c => c.Title).HasColumnOrder(10).IsRequired().HasMaxLength(150).IsUnicode(true).IsVariableLength();
            Property(c => c.Time).HasColumnOrder(15);
            Property(c => c.State).HasColumnOrder(20);


            HasRequired(c => c.TblUser)
                .WithMany(user => user.TblTickets)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);
            
        }
    }
}
