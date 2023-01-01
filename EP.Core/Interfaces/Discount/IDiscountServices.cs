using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Discount
{
    public interface IDiscountServices
    {
        public List<Domain.Entities.Order.Discount> GetAllDiscounts();

        public Domain.Entities.Order.Discount GetDiscountByDiscountId(int discountId);

        public int AddDiscount(Domain.Entities.Order.Discount discount, string startDate, string endDate);

        public void UpdateDiscount(Domain.Entities.Order.Discount discount, string startDate, string endDate);

        public void DeleteDiscount(Domain.Entities.Order.Discount discount);
    }
}
