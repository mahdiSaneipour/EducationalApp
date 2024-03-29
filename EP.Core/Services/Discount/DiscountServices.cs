﻿using EP.Core.Interfaces.Discount;
using EP.Domain.Interfaces.Discount;
using System.Collections.Generic;
using System.Globalization;


namespace EP.Core.Services.Discount
{
    public class DiscountServices : IDiscountServices
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountServices(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public int AddDiscount(Domain.Entities.Order.Discount discount, string startDate, string endDate)
        {

            if (_discountRepository.IsDiscountCodeExist(discount.DiscountCode))
            {
                return 0;
            }

            if (!String.IsNullOrEmpty(startDate))
            {
                string[] std = startDate.Split("/");

                discount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar());
            }

            if (!String.IsNullOrEmpty(endDate))
            {
                string[] edd = endDate.Split("/");

                discount.EndDate = new DateTime(Int32.Parse(edd[0]),
                    Int32.Parse(edd[1]),
                    Int32.Parse(edd[2]),
                    new PersianCalendar());
            }

            _discountRepository.AddDiscount(discount);
            _discountRepository.SaveChanges();

            return discount.DiscountId;
        }

        public List<Domain.Entities.Order.Discount> GetAllDiscounts()
        {
            return _discountRepository.GetAllDiscounts();
        }

        public Domain.Entities.Order.Discount GetDiscountByDiscountId(int discountId)
        {
            return _discountRepository.GetDiscountByDiscountId(discountId);
        }

        public void UpdateDiscount(Domain.Entities.Order.Discount discount, string startDate, string endDate)
        {

            if (_discountRepository.IsDiscountCodeExist(discount.DiscountCode))
            {
                return;
            }

            if (!String.IsNullOrEmpty(startDate))
            {
                string[] std = startDate.Split("/");

                discount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar());
            }

            if (!String.IsNullOrEmpty(endDate))
            {
                string[] edd = endDate.Split("/");

                discount.EndDate = new DateTime(Int32.Parse(edd[0]),
                    Int32.Parse(edd[1]),
                    Int32.Parse(edd[2]),
                    new PersianCalendar());
            }

            _discountRepository.UpdateDiscount(discount);
            _discountRepository.SaveChanges();
        }

        public void DeleteDiscount(Domain.Entities.Order.Discount discount)
        {
            _discountRepository.RemoveDiscount(discount);
            _discountRepository.SaveChanges();
        }
    }
}
