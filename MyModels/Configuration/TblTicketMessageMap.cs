using System.Data.Entity.ModelConfiguration;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblTicketMessageMap : EntityTypeConfiguration<TblTicketMessage>
    {
        public TblTicketMessageMap()
        {

            ToTable("iTblTicketMessage");

            HasKey(c => c.Id);

            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);
            Property(c => c.UserId).HasColumnOrder(3);
            Property(c => c.TicketId).HasColumnOrder(5);
            Property(c => c.Message).HasColumnOrder(10).IsRequired().HasMaxLength(1500).IsUnicode(true).IsVariableLength();
            Property(c => c.IsQuestion).HasColumnOrder(15); 
            Property(c => c.Time).HasColumnOrder(20);
            Property(c => c.State).HasColumnOrder(25);


            HasRequired(c => c.TblUser)
                .WithMany(user => user.TblTicketMessages)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.TblTicket)
                .WithMany(ticket => ticket.TblTicketMessages)
                .HasForeignKey(c => c.TicketId)
                .WillCascadeOnDelete(false);

        }
    }
}
