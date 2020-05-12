using System.Collections.Generic;
using System.Linq;
using MyModels.DAL;
using MyModels.Entity;
using MyModels.Models;
using MyModels.ViewModel;
using MyServices.Base;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class ConstService :GenericRepository<TblConst>, IConstService
    {
        public ConstService(DatabaseContext context)
            : base(context)
        {
        }

        public void Save(TblConst newRecord, int userId)
        {

            var exist = GetAll(a => a.Id == newRecord.Id).Any();

            if (exist)
            {
                Update(newRecord, userId);
            }
            else
            {
                Insert(newRecord, userId);
            }

        }

        /// <summary>
        /// کمبوی ثابت ها
        /// </summary>
        /// <param name="pId">شناسه دسته بندی</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetDataTable(int? pId)
        {
            var temp = DbSet.Where(a => a.PId == pId).OrderBy(a => a.Priority).Select(c => new
            {
                c.Id,
                c.ConstName,
                c.PId,
                c.Description,
                c.ImageFileId,
                ImageUrl = c.TblFileImage.FileUrl,
                c.Priority,
                c.State,

                ConstNumber = c.TblConsts.Count,
                TblConsts = 
                    c.TblConsts.Select(a=> new
                    {
                        a.Id,
                        a.ConstName,
                        a.PId,
                        a.Description,
                        a.ImageFileId,
                        a.Priority,
                        ImageUrl = a.TblFileImage.FileUrl,
                        c.State,
                    }),
                
            });
            return temp;
        }

        /// <summary>
        /// کمبوی ثابت ها
        /// </summary>
        /// <param name="pId">شناسه دسته بندی</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetSelectOptions(int pId)
        {
            var temp = DbSet.Where(a => a.PId == pId).OrderBy(a => a.Priority).Select(c => new
            {
                c.Id,
                Text = c.ConstName,
                ImageUrl = c.TblFileImage.FileUrl
            });
            return temp;
        }

        public IEnumerable<dynamic> GetLast(int lastUpdate)
        {
            var temp = DbSet.Where(c => c.LastUpdate > lastUpdate).Select(c => new
            {
                c.Id,
                c.ConstName,
                c.PId,
                c.Priority,
                c.State,
                c.LastUpdate,
            });
            return temp.ToList();
        }


        //public PagedResults GetAllConst(int pageNumber, int pageSize , string constName, string parentName)
        public ResponsePagingViewModel GetAllConst(RequestPagingViewModel con)
        {
            if (con.PageSize < 1)
            {
                return null;
            }
            //var pId = con.PId;

            var temp = DbSet.AsNoTracking().AsQueryable()
                .Where(a => a.PId == con.PId);

                if (!string.IsNullOrEmpty(con.ConstName))
                {
                    temp = temp.Where(a => a.ConstName.Contains(con.ConstName));
                }
                //if (con.SearchModel.PId.HasValue)
                //{
                //    temp = temp.Where(a => a.PId == con.SearchModel.PId);
                //}


            var count = temp.Count();

            var countPages = (count / con.PageSize) + (count % con.PageSize > 0 ? 1 : 0);
            var result = new ResponsePagingViewModel();
            //result = con;

            if (con.PageNumber > countPages || con.PageNumber < 1)
            {
                con.PageNumber = 1;
            }

            if (con.PageNumber < 7)
            {
                result.Pages = Enumerable.Range(1, (countPages > 11 ? 11 : countPages)).ToArray();
            }
            else if (con.PageNumber > (countPages - 6))
            {
                result.Pages = Enumerable.Range((countPages - 10), 11).ToArray();
            }
            else
            {
                result.Pages = Enumerable.Range((con.PageNumber - 5), 11).ToArray();
            }

            //result.PageNumber = con.PageNumber;
            //result.PageSize = con.PageSize;
            result.TotalNumberOfPages = countPages;

            //pageNumber--;
            var start = (con.PageNumber - 1) * con.PageSize;
            result.Results = temp.OrderByDescending(a => a.Id).Skip(start).Take(con.PageSize).Select(a => new
            {
                a.Id,
                a.PId,
                a.ConstName,
                //Parent = a.Parent.ConstName,
                a.Priority,
                a.State,
                a.Description
            }).AsEnumerable();

            return result;
        }

        //public void Delete(int[] ids)
        //{
        //    DbSet.RemoveRange(DbSet.Where(a=> ids.Any(b=> b== a.Id)));
        //}
        public override void Delete(int id, int userId)
        {
            DbSet.RemoveRange(DbSet.Where(a => a.PId == id));
            base.Delete(id, userId);
        }

    }
}