using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
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
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            PhotoRepositry = new PhotoRepositry(db);
            ProductRepositry = new ProductRepositry(db);
            CategoryRepositry = new CategoryRepositry(db);  
        }
        public IPhotoRepositry PhotoRepositry {  get; private set; }

        public IProductRepositry ProductRepositry { get; private set; }

        public ICategoryRepositry CategoryRepositry { get; private set; }
    }
}
