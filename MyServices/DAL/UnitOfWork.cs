using System;
using MyModels.DAL;
using MyServices.Interfaces;
using MyServices.Services;

namespace MyServices.DAL
{

    public class UnitOfWork : IDisposable
    {
        private readonly DatabaseContext _context ;//= new DatabaseContext();

        public UnitOfWork()
        {
            _context = new DatabaseContext();
        }
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        private ICategoryService _categoryService;
        private ICityService _cityService;
        private IConstService _constService;
        private IBrandService _brandService;
        private INotificationsService _notificationsService;
        private IPostService _postService;
        private IReportAbuseService _reportAbuseService;
        private IServiceService _serviceService;
        private IPostDeleteReasonService _postDeleteReasonService;
        private ITicketService _ticketService;
        private ITicketMessageService _ticketMessageService;
        private IShoppingService _shoppingService;
        private IUpdateService _updateService;
        private IStatisticService _statisticService;
        private IChargeService _ChargeService;
        private ISpeakerService _SpeakerService;
        private ISpeakerRequestService _SpeakerRequestService;
        private ISpeechFieldService _SpeechFieldService;
        private IFileService _FileService;
        private IUserInfoService _UserInfoService;
        //private GenericRepository<Course> courseRepository;

        public ICategoryService CategoryRepository
        {
            get
            {

                if (_categoryService == null)
                {
                    _categoryService = new CategoryService(_context);
                }
                return _categoryService;
            }
        }
        
        public ICityService CityRepository
        {
            get
            {

                if (_cityService == null)
                {
                    _cityService = new CityService(_context);
                }
                return _cityService;
            }
        }
        
        public IConstService ConstRepository
        {
            get
            {

                if (_constService == null)
                {
                    _constService = new ConstService(_context);
                }
                return _constService;
            }
        }
        
        public IBrandService BrandRepository
        {
            get
            {

                if (_brandService == null)
                {
                    _brandService = new BrandService(_context);
                }
                return _brandService;
            }
        }

        public INotificationsService NotificationsRepository
        {
            get
            {

                if (_notificationsService == null)
                {
                    _notificationsService = new NotificationsService(_context);
                }
                return _notificationsService;
            }
        } 
        
        public IPostService PostRepository
        {
            get
            {

                if (_postService == null)
                {
                    _postService = new PostService(_context);
                }
                return _postService;
            }
        }

        public IReportAbuseService ReportAbuseRepository
        {
            get
            {

                if (_reportAbuseService == null)
                {
                    _reportAbuseService = new ReportAbuseService(_context);
                }
                return _reportAbuseService;
            }
        }

        public IServiceService ServiceRepository
        {
            get
            {

                if (_serviceService == null)
                {
                    _serviceService = new ServiceService(_context);
                }
                return _serviceService;
            }
        }

        public IPostDeleteReasonService PostDeleteReasonRepository => _postDeleteReasonService ?? (_postDeleteReasonService = new PostDeleteReasonService(_context));

        public ITicketService TicketRepository => _ticketService ?? (_ticketService = new TicketService(_context));

        public ITicketMessageService TicketMessageRepository => _ticketMessageService ?? (_ticketMessageService = new TicketMessageService(_context));

        public IShoppingService ShoppingRepository => _shoppingService ?? (_shoppingService = new ShoppingService(_context));
        public IUpdateService UpdateRepository => _updateService ?? (_updateService = new UpdateService(_context));
        public IStatisticService StatisticRepository => _statisticService ?? (_statisticService = new StatisticService(_context));
        public IChargeService ChargeRepository => _ChargeService ?? (_ChargeService = new ChargeService(_context));

        public ISpeakerService SpeakerRepository
        {
            get
            {

                if (_SpeakerService == null)
                {
                    _SpeakerService = new SpeakerService(_context);
                }
                return _SpeakerService;
            }
        }
        public ISpeakerRequestService SpeakerRequestRepository
        {
            get
            {

                if (_SpeakerRequestService == null)
                {
                    _SpeakerRequestService = new SpeakerRequestService(_context);
                }
                return _SpeakerRequestService;
            }
        }

        public ISpeechFieldService SpeechFieldRepository
        {
            get
            {

                if (_SpeechFieldService == null)
                {
                    _SpeechFieldService = new SpeechFieldService(_context);
                }
                return _SpeechFieldService;
            }
        }

        public IFileService FileRepository
        {
            get
            {

                if (_FileService == null)
                {
                    _FileService = new FileService(_context);
                }
                return _FileService;
            }
        }

        public IUserInfoService UserInfoRepository
        {
            get
            {

                if (_UserInfoService == null)
                {
                    _UserInfoService = new UserInfoService(_context);
                }
                return _UserInfoService;
            }
        }

        //public GenericRepository<Course> CourseRepository
        //{
        //    get
        //    {

        //        if (this.courseRepository == null)
        //        {
        //            this.courseRepository = new GenericRepository<Course>(context);
        //        }
        //        return courseRepository;
        //    }
        //}

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
