using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblCityMap : EntityTypeConfiguration<TblCity>
    {
        public TblCityMap()
        {
            // Note: Attribute is better!
            //نام جدول چه باشد
            ToTable("iTblCity");

            // Note: Attribute is better!
            //پرایمری نمودن آی دی
            HasKey(c => c.Id);

            // Note: Attribute is better!
            //آی دی اتو اینکریمنت باشد
            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(0);
            Property(c => c.CityName).HasColumnOrder(1).IsRequired().HasMaxLength(70).IsUnicode(true).IsVariableLength();
            Property(c => c.PId).HasColumnOrder(2);
            Property(c => c.State).HasColumnOrder(5);
            Property(c => c.LastUpdate).HasColumnOrder(6);
            Property(c => c.Priority).HasColumnOrder(15);


            // Note: Fluent Api is better!
            //فلوانت ای پی آی برای ایجاد رابطه یک به چند
            //در این قسمت میتوان کسکید دلیت رو هم فالس گذاشتیم و لی در اتربیوت نمیتوان
            HasOptional(c => c.Parent)
                .WithMany(country => country.TblCities)
                .HasForeignKey(c => c.PId)
                .WillCascadeOnDelete(false);

        }
    }
}
