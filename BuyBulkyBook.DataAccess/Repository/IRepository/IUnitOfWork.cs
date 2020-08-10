using System;
using System.Collections.Generic;
using System.Text;

namespace BuyBulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        IProductRepository Product { get; }
        ICoverTypeRepository CoverType { get; }
        ICategoryRepository Category { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
