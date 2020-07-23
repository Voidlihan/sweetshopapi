using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Infrastructure.Database.Interfaces;
using Infrastructure.EntityFramework;

namespace Infrastructure.Database.EFImplementations
{
    public class OrderDetailsRepository : IRepository<OrderDetail>
    {
        private readonly SweetShopDataContext _context;

        public OrderDetailsRepository(SweetShopDataContext context)
        {
            _context = context;
        }

        public OrderDetail Get(int id)
        {
            return _context.OrderDetails.Find(id);
        }

        public IList<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetail Create(OrderDetail entity)
        {
            var orderDetail = _context.OrderDetails.Add(entity);
            return orderDetail;
        }

        public OrderDetail Edit(OrderDetail entity)
        {
            var orderDetail = _context.OrderDetails.Find(entity.Id);
            if (orderDetail != null)
            {
                orderDetail = entity;
            }
            return orderDetail;
        }

        public OrderDetail Delete(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            return _context.OrderDetails.Remove(orderDetail);
        }
    }
}