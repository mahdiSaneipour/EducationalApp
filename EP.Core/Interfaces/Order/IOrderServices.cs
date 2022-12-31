using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Order
{
    public interface IOrderServices
    {
        public int AddOrder(int userId, int courseId);

        public Domain.Entities.Order.Order GetOrderByUserIdAndOrderId(int userId, int orderId);

        public List<Domain.Entities.Order.Order> GetOrdersByUserId(int userId);

        public bool UseDiscount(int orderId, int userId, string discountCode);

        public bool FinalOrder(int userId, int orderId);
    }
}
