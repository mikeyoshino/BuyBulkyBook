using BuyBulkyBook.DataAccess.Data;
using BuyBulkyBook.DataAccess.Repository;
using BuyBulkyBook.DataAccess.Repository.IRepository;
using BuyBulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuyBulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            var objFromDb = _db.Companies.FirstOrDefault(s => s.Id == company.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = company.Name;
                objFromDb.City = company.City;
                objFromDb.PhoneNumber = company.PhoneNumber;
                objFromDb.StreetAddress = company.StreetAddress;
                objFromDb.Stage = company.Stage;
                objFromDb.IsAuthorizedCompany = company.IsAuthorizedCompany;
                objFromDb.PostalCode = company.PostalCode;
            }
        }
    }
}
