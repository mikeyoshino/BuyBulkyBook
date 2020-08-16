using BuyBulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyBulkyBook.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company company);
    }
}
