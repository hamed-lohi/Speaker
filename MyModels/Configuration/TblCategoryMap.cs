using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Entity;

namespace MyModels.Configuration
{
    internal class TblCategoryMap: EntityTypeConfiguration<TblCategory>
    {
        public TblCategoryMap()
        {
            // Note: Attribute is better!
            //نام جدول چه باشد
            ToTable("iTblCategory");

            // Note: Attribute is better!
            //پرایمری نمودن آی دی
            HasKey(c => c.Id);

            // Note: Attribute is better!
            //آی دی اتو اینکریمنت باشد
            Property(current => current.Id)
                .HasDatabaseGeneratedOption
                (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(0);
            Property(c => c.CategoryName).HasColumnOrder(1).IsRequired().HasMaxLength(60).IsUnicode(true).IsVariableLength();
            Property(c => c.PId).HasColumnOrder(2);
            Property(c => c.IconUrl).HasColumnOrder(3).HasMaxLength(150).IsUnicode(false).IsVariableLength();
            Property(c => c.Priority).HasColumnOrder(4);
            Property(c => c.State).HasColumnOrder(5);
            Property(c => c.LastUpdate).HasColumnOrder(6);
            Property(c => c.BrandId).HasColumnOrder(8);
            Property(c => c.SSTradeType).HasColumnOrder(10);
            Property(c => c.SSStock).HasColumnOrder(15);
            Property(c => c.SSSaleOrBuy).HasColumnOrder(20);
            Property(c => c.SSType).HasColumnOrder(22);
            Property(c => c.SSType2).HasColumnOrder(25);
            Property(c => c.SSType3).HasColumnOrder(27);
            Property(c => c.SSGender).HasColumnOrder(30);
            Property(c => c.HelpText).HasColumnOrder(35).HasMaxLength(600).IsUnicode(true).IsVariableLength();
            Property(c => c.Description).HasColumnOrder(45).HasMaxLength(1000).IsUnicode(true).IsVariableLength();


            // Note: Fluent Api is better!
            //فلوانت ای پی آی برای ایجاد رابطه یک به چند
            //در این قسمت میتوان کسکید دلیت رو هم فالس گذاشتیم و لی در اتربیوت نمیتوان
            HasOptional(c => c.Parent)
                .WithMany(country => country.TblCategories)
                .HasForeignKey(c => c.PId)
                .WillCascadeOnDelete(false);

            HasOptional(c => c.TblBrand)
                .WithMany(brand => brand.TblCategories)
                .HasForeignKey(c => c.BrandId)
                .WillCascadeOnDelete(false);

            //خودش
            //HasRequired(current => current.Country)
            //طرف مقابل
            //.WithMany(country => country.States)
            //خودش
            //.HasForeignKey(current => current.CountryID)
            //.WillCascadeOnDelete(false)
            //;

            //HasOptional(current => current.Country)
            //	.WithMany(country => country.States)
            //	.HasForeignKey(current => current.CountryID)
            //	.WillCascadeOnDelete(false)
            //	;

            //ترکیب فلوانت ای پی آی برای  ایجاد انواع مختلف 

            // Char(x):
            //.IsUnicode(false)
            //.HasMaxLength(x)
            //.IsFixedLength()

            // nChar(x):
            //.IsUnicode(true)
            //.HasMaxLength(x)
            //.IsFixedLength()

            // VarChar(x):
            //.IsUnicode(false)
            //.HasMaxLength(x)
            //.IsVariableLength()

            // nVarChar(x):
            //.IsUnicode(true)
            //.HasMaxLength(x)
            //.IsVariableLength()

            // VarChar(Max):
            //.IsUnicode(false)
            //.IsMaxLength()
            //.IsVariableLength()

            // nVarChar(Max):
            //.IsUnicode(true)
            //.IsMaxLength()
            //.IsVariableLength()

            // توصيه استاد داریوش تصدیقی
            // استفاده نماييد Fluent Api فقط برای حالات ذيل از

            // Char(x):
            //.IsUnicode(false)
            //.HasMaxLength(x)
            //.IsFixedLength()

            // nChar(x):
            //.IsUnicode(true)
            //.HasMaxLength(x)
            //.IsFixedLength()

            // VarChar(x):
            //.IsUnicode(false)
            //.HasMaxLength(x)
            //.IsVariableLength()

            // VarChar(Max):
            //.IsUnicode(false)
            //.IsMaxLength()
            //.IsVariableLength()
        }
    }
}
