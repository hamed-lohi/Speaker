using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.DAL;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class SpeechFieldService :GenericRepository<TblSpeechField>, ISpeechFieldService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public SpeechFieldService(DatabaseContext context)
            : base(context)
        {
        }

    }
}