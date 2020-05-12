using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.Interfaces;
using utility.Date;

namespace MyServices.Services
{
    public class SMSService : ISMSService
    {

        #region variable

        
        private DatabaseContext Context;
        private DbSet<TblSMS> DbSet;
        private DbSet<TblUser> TblUser;
        #endregion


        public SMSService(DatabaseContext context)
        {
            Context = context;
            DbSet = Context.Set<TblSMS>();
            TblUser = Context.Set<TblUser>();
        }

        public void Send(string tel)
        {
            //var rnd = new Random();
            //var code = rnd.Next(10000, 99999);
            //var userId = TblUser.Where(u => u.Tel == tel).Select(u => u.Id).FirstOrDefault();
            //var isDuplicate = DbSet.Any(s => s.Tel == tel && (GetDateTime.CurrentTimeSeconds() - s.SendTime) < 300);

            //if (isDuplicate)
            //{
            //    // کد قبلا برای شما ارسال شده است
            //}




            //var temp = DbSet.AsNoTracking().Where(c => c.LastUpdate > lastUpdate && c.UserId == userId).Select(c => new
            //{
            //    c.Id,
            //    c.UserId,
            //    c.Title,
            //    c.Description,
            //    c.Date,
            //    c.State,
            //    c.LastUpdate,
            //});
            //return temp.ToList();
        }
    }
}