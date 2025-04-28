using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IPhotoRepositry PhotoRepositry { get; }
        public IProductRepositry ProductRepositry { get; }
        public ICategoryRepositry CategoryRepositry { get; }
        public ICustomerBasketRepositry CustomerBasket  { get; }
        public IAuth Auth { get; }
    }
}
