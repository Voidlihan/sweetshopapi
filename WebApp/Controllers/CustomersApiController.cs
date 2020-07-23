using Domain.Model;
using Infrastructure.Database.EFImplementations;
using Infrastructure.Database.Interfaces;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class CustomersApiController : ApiController
    {
        private readonly IUnitOfWork _uow;

        public CustomersApiController()
        {
            var db = new SweetShopDataContext();
            var orders = new OrdersRepository(db);
            var orderDetails = new OrderDetailsRepository(db);
            var products = new ProductsRepository(db);
            var customers = new CustomersRepository(db);
            var uow = new EFUnitOfWork(products, orders, orderDetails, customers, db);

            _uow = uow;
        }

        // GET: api/CustomersApi
        public IEnumerable<Customer> Get()
        {
            return _uow.Customers.GetAll();
        }

        // GET: api/CustomersApi/5
        public Customer Get(int id)
        {
            return _uow.Customers.Get(id);
        }

        // POST: api/CustomersApi
        public Customer Post([FromBody]Customer customer)
        {
            var savedCustomer = _uow.Customers.Create(customer);
            _uow.Save();
            return savedCustomer;
        }

        // PUT: api/CustomersApi/5
        public void Put(int id, [FromBody]string value)
        {
            var customer = _uow.Customers.Get(id);
            _uow.Customers.Create(customer);
            _uow.Save();
        }

        // DELETE: api/CustomersApi/5
        public void Delete(int id)
        {
            var customer = _uow.Customers.Get(id);
            _uow.Products.Delete(id);
        }
    }
}
