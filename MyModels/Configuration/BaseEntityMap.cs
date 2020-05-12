using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModels.Configuration
{
    internal class BaseEntityMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<BaseEntity>
    {
         public BaseEntityMap()
        {

            Property(c => c.InsertDate).HasColumnOrder(100);
            Property(c => c.UpdateDate).HasColumnOrder(101);
            Property(c => c.InsertUserId).HasColumnOrder(102);
            Property(c => c.UpdateUserId).HasColumnOrder(103);
            Property(c => c.ActionType).HasColumnOrder(104);

        }
    }
}
