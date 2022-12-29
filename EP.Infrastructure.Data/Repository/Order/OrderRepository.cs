using EP.Domain.Entities.Order;
using EP.Domain.Interfaces.Course;
using EP.Domain.Interfaces.Order;
using EP.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EPContext _context;

        public OrderRepository(EPContext context)
        {
            _context = context;
        }

        public void AddOrder(Domain.Entities.Order.Order order)
        {
            _context.Orders.Add(order);
        }

        public void AddOrderDetail(OrderDetails orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
        }

        public Domain.Entities.Order.Order GetOpenOrderByUserId(int userId)
        {
            return _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
        }



        public void UpdateOrder(Domain.Entities.Order.Order order)
        {
            _context.Orders.Update(order);
        }

        public OrderDetails GetOrderDetailByOrderIdAndCourseId(int orderId, int courseId)
        {
            return _context.OrderDetails.FirstOrDefault(o => o.OrderId == orderId && o.CourseId == courseId);
        }

        public void UpdateOrderDetail(OrderDetails orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }

        public int GetOrderSum(int orderId)
        {
            return _context.OrderDetails.Where(o => o.OrderId == orderId).Sum(o => o.Price * o.Count);
        }

        public void saveChanges()
        {
            _context.SaveChanges();
        }
    }
}
