using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MyModels.Configuration;
using MyModels.Entity;
using MyModels.Models;

namespace MyModels.DAL
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>    //DbContext
    {
        static DatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer(new DatabaseContextInitializer());
        }

        //public DatabaseContext()
        //    : base("DatabaseCon") // connectionString name
        //{
            
        //}


        public DatabaseContext()
            : base("DatabaseCon")//DefaultConnection, throwIfV1Schema: false
        {
        }

        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }


        public DbSet<TblCategory> TblCategories { get; set; }
        public DbSet<TblCity> TblCities { get; set; }
        public DbSet<TblConst> TblConsts { get; set; }
        public DbSet<TblBrand> TblBrands { get; set; }
        public DbSet<TblNotifications> TblNotificationss { get; set; }
        //public DbSet<ApplicationUser> TblUsers { get; set; }
        //public DbSet<TblSMS> TblSMSs { get; set; }
        public DbSet<TblPost> TblPosts { get; set; }
        public DbSet<TblReportAbuse> TblReportAbuses { get; set; }
        public DbSet<TblService> TblServices { get; set; }
        public DbSet<TblPostDeleteReason> TblPostDeleteReasons { get; set; }
        public DbSet<UserRole> TblUserRoles { get; set; }
        public DbSet<TblTicket> TblTickets { get; set; }
        public DbSet<TblTicketMessage> TblTicketMessages { get; set; }
        public DbSet<TblShopping> TblShoppings { get; set; }
        public DbSet<TblUpdate> TblUpdates { get; set; }
        public DbSet<TblStatistic> TblStatistics { get; set; }
        public DbSet<TblCharge> TblCharges { get; set; }
        public DbSet<TblSpeaker> TblSpeakers { get; set; }
        public DbSet<TblSpeakerRequest> TblSpeakerRequests { get; set; }
        public DbSet<TblSpeechField> TblSpeechFields { get; set; }
        public DbSet<TblFile> TblFiles { get; set; }
        public DbSet<TblPermission> TblPermissions { get; set; }


        /// <summary>
        /// متد اورراید آن مدل کریتینگ 
        /// در این متد کانفیگور های موجود در کلاس های مدل را به برنامه اد میکنیم
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Configurations.Add(new TblCategoryMap());
            modelBuilder.Configurations.Add(new TblCityMap());
            modelBuilder.Configurations.Add(new TblConstMap());
            modelBuilder.Configurations.Add(new TblBrandMap());
            modelBuilder.Configurations.Add(new TblNotificationsMap());
            //modelBuilder.Configurations.Add(new TblUserMap());
            //modelBuilder.Configurations.Add(new TblSMSMap());
            modelBuilder.Configurations.Add(new TblPostMap());
            modelBuilder.Configurations.Add(new TblReportAbuseMap());
            modelBuilder.Configurations.Add(new TblServiceMap());
            modelBuilder.Configurations.Add(new TblPostDeleteReasonMap());
            modelBuilder.Configurations.Add(new TblTicketMap());
            modelBuilder.Configurations.Add(new TblTicketMessageMap());
            modelBuilder.Configurations.Add(new TblShoppingMap());
            modelBuilder.Configurations.Add(new TblUpdateMap());
            modelBuilder.Configurations.Add(new TblStatisticMap());
            modelBuilder.Configurations.Add(new TblChargeMap());
            modelBuilder.Configurations.Add(new TblSpeakerMap());
            modelBuilder.Configurations.Add(new TblSpeakerRequestMap());
            modelBuilder.Configurations.Add(new TblSpeechFieldMap());
            modelBuilder.Configurations.Add(new TblFileMap());
            modelBuilder.Configurations.Add(new TblUserInfoMap());
            modelBuilder.Configurations.Add(new TblPermissionMap());


            //modelBuilder.Entity<TblUser>().ToTable("iTblUser");//.Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationUser>().ToTable("iTblUser");//.Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<Role>().ToTable("iTblRoles");
            modelBuilder.Entity<UserRole>().ToTable("iTblUserRoles");
            modelBuilder.Entity<UserClaim>().ToTable("iTblUserClaims");
            modelBuilder.Entity<UserLogin>().ToTable("iTblUserLogins");


        }

    }
}
