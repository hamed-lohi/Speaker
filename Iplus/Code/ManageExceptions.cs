using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Iplus.Code
{
    public static class ManageExceptions
    {

        public static void HandleException(Exception exception)
        {
            DbUpdateConcurrencyException concurrencyEx = exception as DbUpdateConcurrencyException;
            if (concurrencyEx != null)
            {
                // A custom exception of yours for concurrency issues
                //throw new ConcurrencyException();

            }

            DbUpdateException dbUpdateEx = exception as DbUpdateException;

            if (dbUpdateEx == null || dbUpdateEx.InnerException == null || dbUpdateEx.InnerException.InnerException == null) 
                return;

            SqlException sqlException = dbUpdateEx.InnerException.InnerException as SqlException;

            if (sqlException == null) 
                //throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);
                throw new Exception("عدم دسترسی به پایگاه داده");

            switch (sqlException.Number)
            {
                case 2627:  // Unique constraint error
                case 547:   // Constraint check violation
                case 2601:  // Duplicated key row error
                    // Constraint violation exception
                    //throw new ConcurrencyException();   // A custom exception of yours for concurrency issues
                    throw new Exception("خطای هم زمانی");
                default:
                    // A custom exception of yours for other DB issues
                    //throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);
                    throw new Exception("عدم دسترسی به پایگاه داده");
            }

            //throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);

            // If we're here then no exception has been thrown
            // So add another piece of code below for other exceptions not yet handled...
        }



        public static void CustomException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.NotFound, string mediaType = "text/plain")
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(message, System.Text.Encoding.UTF8, mediaType),
                StatusCode = httpStatusCode
            };

        throw new HttpResponseException(response);
        }
        



    }

}