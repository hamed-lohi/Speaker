using System.Collections.Generic;
using MyModels.Entity;

namespace MyModels.ViewModel
{
    public class RequestPagingViewModel
    {
        /// <summary>
        /// The page number this page represents.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The size of this page.
        /// </summary>
        public int PageSize { get; set; }


        public int? Id { get; set; }
        public int? PId { get; set; }
        public string ConstName { get; set; }
        public int? Priority { get; set; }
        public byte? State { get; set; }
        public string Description { get; set; }
        public bool? Selected { get; set; }

        /// <summary>
        /// The URL to the next page - if null, there are no more pages.
        /// </summary>
        //public string NextPageUrl { get; set; }
    }
}