using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        private readonly IConnectionMultiplexer _redis;

        public UnitOfWork(AppDbContext db, IMapper mapper, IImageManagementService imageManagementService, IConnectionMultiplexer redis)
        {
            _db = db;
            _mapper = mapper;
            _redis = redis;

            _imageManagementService = imageManagementService;
            PhotoRepositry = new PhotoRepositry(db);
            ProductRepositry = new ProductRepositry(db, mapper, imageManagementService);
            CategoryRepositry = new CategoryRepositry(db);
            CustomerBasket = new CustomerBasketRepositry(redis);
        }
        public IPhotoRepositry PhotoRepositry {  get; private set; }

        public IProductRepositry ProductRepositry { get; private set; }

        public ICategoryRepositry CategoryRepositry { get; private set; }

        public ICustomerBasketRepositry CustomerBasket { get; private set; }
    }
}
