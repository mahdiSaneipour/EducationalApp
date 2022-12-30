﻿using EP.Core.Interfaces.Order;
using EP.Domain.Interfaces.Course;
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

        public OrderServices(IOrderRepository orderRepository,
            ICourseRepository courseRepository, IWalletRepository walletRepository)
        {
            _orderRepository = orderRepository;
            _courseRepository = courseRepository;
            _walletRepository = walletRepository;
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

                _walletRepository.SaveChanges();

                return true;
            }

            return false;
        }

    }
}