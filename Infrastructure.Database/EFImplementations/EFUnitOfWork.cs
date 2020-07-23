using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Infrastructure.Database.Interfaces;
using Infrastructure.EntityFramework;

namespace Infrastructure.Database.EFImplementations
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly SweetShopDataContext _context;

        public IRepository<Product> Products { get; set; }
        public IRepository<Order> Orders { get; set; }
        public IRepository<OrderDetail> OrderDetails { get; set; }
        public IRepository<Customer> Customers { get; set; }

        public EFUnitOfWork(IRepository<Product> products, 
            IRepository<Order> orders, 
            IRepository<OrderDetail> orderDetails,
            IRepository<Customer> customers,
            SweetShopDataContext context)
        {
            Products = products;
            Orders = orders;
            OrderDetails = orderDetails;
            Customers = customers;
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                _context.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                _context.Database.CurrentTransaction.Commit();
            }
        }

        public void Rollback()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                _context.Database.CurrentTransaction.Rollback();
            }
        }
    }
}
