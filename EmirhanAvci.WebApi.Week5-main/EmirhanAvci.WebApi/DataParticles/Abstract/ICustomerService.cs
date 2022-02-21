using EmirhanAvci.WebApi.Helpers.Paging;
using EmirhanAvci.WebApi.Helpers.Paging.Model;
using EmirhanAvci.WebApi.Models;
using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.DataParticles.Abstract
{
    public interface ICustomerService
    {
        PagingResultModel<Customer> GetCustomers(PagingQueryParams pagingyParams);
        List<Customer> GetAll();
    }

    public class CustomerManager : ICustomerService
    {
        private readonly Context _db;

        public CustomerManager(Context db)
        {
            _db = db;
        }
        public List<Customer> GetAll()
        {
            return _db.Customers.ToList();
        }
        public PagingResultModel<Customer> GetCustomers(PagingQueryParams pagingyParams)
        {
            PagingResultModel<Customer> customers = new PagingResultModel<Customer>(pagingyParams);
            customers.GetData(_db.Customers);
            return customers;
        }
    }
}
