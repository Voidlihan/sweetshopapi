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
    public class OrdersApiController : ApiController
    {
        private readonly IUnitOfWork _uow;

        public OrdersApiController()
        {
            var db = new SweetShopDataContext();
            var orders = new OrdersRepository(db);
            var orderDetails = new OrderDetailsRepository(db);
            var products = new ProductsRepository(db);
            var customers = new CustomersRepository(db);
            var uow = new EFUnitOfWork(products, orders, orderDetails, customers, db);

            _uow = uow;
        }

        // GET: api/OrdersApi
        public IEnumerable<Order> Get()
        {
            return _uow.Orders.GetAll();
        }

        // GET: api/OrdersApi/5
        public Order Get(int id)
        {
            return _uow.Orders.Get(id);
        }

        // POST: api/OrdersApi
        public Order Post([FromBody]Order order)
        {
            var savedOrder = _uow.Orders.Create(order);
            _uow.Save();
            return savedOrder;
        }

        // PUT: api/OrdersApi/5
        public void Put(int id, [FromBody]string value)
        {
            var order = _uow.Orders.Get(id);
            _uow.Orders.Create(order);
            _uow.Save();
        }

        // DELETE: api/OrdersApi/5
        public void Delete(int id)
        {
            var order = _uow.Orders.Get(id);
            _uow.Orders.Delete(id);
        }
    }
}
