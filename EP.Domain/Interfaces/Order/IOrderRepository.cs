using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Order
{
    public interface IOrderRepository
    {
        public Domain.Entities.Order.Order GetOpenOrderByUserId(int userID);

        public Domain.Entities.Order.OrderDetails GetOrderDetailByOrderIdAndCourseId(int orderId, int courseId);

        public int GetOrderSum(int orderId);

        public void AddOrder(Domain.Entities.Order.Order order);

        public void AddOrderDetail(Domain.Entities.Order.OrderDetails orderDetail);

        public void UpdateOrder(Domain.Entities.Order.Order order);

        public void UpdateOrderDetail(Domain.Entities.Order.OrderDetails orderDetail);

        public void saveChanges();
    }
}
