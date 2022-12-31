﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Discount
{
    public interface IDiscountRepository
    {
        public Domain.Entities.Order.Discount GetDiscountByDiscountCode(string discountCode);

        public void UpdateDiscount(Domain.Entities.Order.Discount discount);

        public int AddDiscount(Domain.Entities.Order.Discount discount);

        public Domain.Entities.Order.Discount GetDiscountByDiscountId(int discountId);

        public void SaveChanges();

    }
}
