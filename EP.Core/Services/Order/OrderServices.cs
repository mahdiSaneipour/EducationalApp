using EP.Core.Interfaces.Order;
using EP.Domain.Interfaces.Course;
using EP.Domain.Interfaces.Discount;
using EP.Domain.Interfaces.Order;
using EP.Domain.Interfaces.User;
using EP.Domain.Interfaces.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.Order
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDiscountRepository _discountRepository;

        public OrderServices(IOrderRepository orderRepository,
            ICourseRepository courseRepository, IWalletRepository walletRepository,
            IUserRepository userRepository, IDiscountRepository discountRepository)
        {
            _orderRepository = orderRepository;
            _courseRepository = courseRepository;
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _discountRepository = discountRepository;
        }

        public int AddOrder(int userId, int courseId)
        {
            Domain.Entities.Order.Order order = _orderRepository.GetOpenOrderByUserId(userId);
            int price = _courseRepository.GetCoursePriceByCourseId(courseId);

            if (order == null) {
                order = new Domain.Entities.Order.Order() { 
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    UserId = userId,
                    OrderSum = price,
                    OrderDetails = new List<Domain.Entities.Order.OrderDetails>()
                    {
                        new Domain.Entities.Order.OrderDetails()
                        {
                            CourseId = courseId,
                            Price = price,
                            Count = 1,
                        }
                    }
                };

                _orderRepository.AddOrder(order);
                _orderRepository.saveChanges();

            }
            else
            {
                Domain.Entities.Order.OrderDetails orderDetails =
                    _orderRepository.GetOrderDetailByOrderIdAndCourseId(order.OrderId, courseId);

                if (orderDetails == null)
                {
                    orderDetails = new Domain.Entities.Order.OrderDetails() {
                        OrderId= order.OrderId,
                        CourseId = courseId,
                        Price = price,
                        Count = 1
                    };
                    _orderRepository.AddOrderDetail(orderDetails);
                } else
                {
                    orderDetails.Count++;
                    _orderRepository.UpdateOrderDetail(orderDetails);
                }

                _orderRepository.saveChanges();

                order.OrderSum = _orderRepository.GetOrderSum(order.OrderId);
                _orderRepository.UpdateOrder(order);

                _orderRepository.saveChanges();
            }


            return order.OrderId;
        }

        public Domain.Entities.Order.Order GetOrderByUserIdAndOrderId(int userId, int orderId)
        {
            Domain.Entities.Order.Order order = _orderRepository.GetOrderByOrderIdAndUserId(userId, orderId);
            return order;
        }

        public bool FinalOrder(int userId, int orderId)
        {
            Domain.Entities.Order.Order order = _orderRepository.GetOrderByOrderIdAndUserId(userId, orderId);

            if (order == null)
            {
                return false;
            }

            int userBalance = _walletRepository.BalanceUserWallet(userId);
            int sumOrder = order.OrderDetails.Sum(od => od.Price * od.Count);

            if (userBalance >= sumOrder)
            {

                Domain.Entities.Wallet.Wallet wallet = new Domain.Entities.Wallet.Wallet()
                {
                    Description = "پرداخت فاکتور #" + orderId,
                    CreateDate = DateTime.Now,
                    Amount = sumOrder,
                    UserId = userId,
                    IsPay = true,
                    TypeId = 2,
                };

                _walletRepository.AddWallet(wallet);

                order.IsFinaly = true;
                _orderRepository.UpdateOrder(order);

                foreach (var courseId in order.OrderDetails.Select(od => od.CourseId))
                {
                    Domain.Entities.User.UserCourses userCourses = new Domain.Entities.User.UserCourses()
                    {
                        UserId = userId,
                        CourseId = courseId
                    };
                    _userRepository.AddUserCourse(userCourses);
                }

                _walletRepository.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UseDiscount(int orderId, int userId, string discountCode)
        {
            Domain.Entities.Order.Order order = _orderRepository.GetOrderByOrderIdAndUserId(userId, orderId);

            if (order == null)
            {
                return false;
            }

            Domain.Entities.Order.Discount discount = _discountRepository.GetDiscountByDiscountCode(discountCode);

            if (discount == null)
            {
                return false;
            }

            if (discount.UsableCount != null && discount.UsableCount < 1)
            {
                return false;
            }

            if (discount.StartDate != null && discount.StartDate > DateTime.Now)
            {
                return false;
            }

            if (discount.EndDate != null && discount.EndDate < DateTime.Now)
            {
                return false;
            }

            int discountetAmount = (order.OrderSum * discount.DicountPercent) / 100;

            if (_discountRepository.DoesUserUsedDiscount(userId, discount.DiscountId))
            {
                return false;
            }

            order.OrderSum = order.OrderSum - discountetAmount;

            if (discount.UsableCount != null)
            {
                discount.UsableCount--;
            }

            Domain.Entities.User.UserDiscounts userDiscounts = new Domain.Entities.User.UserDiscounts()
            {
                UserId = userId,
                DiscountId = discount.DiscountId
            };

            _orderRepository.UpdateOrder(order);
            _discountRepository.UpdateDiscount(discount);
            _discountRepository.AddUserDiscount(userDiscounts);

            _discountRepository.SaveChanges();

            return true;
        }

        public List<Domain.Entities.Order.Order> GetOrdersByUserId(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }
    }
}