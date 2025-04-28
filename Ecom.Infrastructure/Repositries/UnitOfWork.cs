using AutoMapper;
using Ecom.Core.Entites;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public UnitOfWork(AppDbContext db, IMapper mapper, IImageManagementService imageManagementService, IConnectionMultiplexer redis, UserManager<AppUser> userManager = null)
        {
            _db = db;
            _mapper = mapper;
            _redis = redis;
            _userManager = userManager;

            _imageManagementService = imageManagementService;
            PhotoRepositry = new PhotoRepositry(db);
            ProductRepositry = new ProductRepositry(db, mapper, imageManagementService);
            CategoryRepositry = new CategoryRepositry(db);
            CustomerBasket = new CustomerBasketRepositry(redis);
            Auth = new AuthRepositry(userManager);
        }
        public IPhotoRepositry PhotoRepositry {  get; private set; }

        public IProductRepositry ProductRepositry { get; private set; }

        public ICategoryRepositry CategoryRepositry { get; private set; }

        public ICustomerBasketRepositry CustomerBasket { get; private set; }

        public IAuth Auth { get; private set; }
    }
}
