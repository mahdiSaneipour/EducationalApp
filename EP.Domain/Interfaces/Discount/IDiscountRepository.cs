using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Discount
{
    public interface IDiscountRepository
    {
        public bool IsDiscountCodeExist(string discountCode);

        public Domain.Entities.Order.Discount GetDiscountByDiscountCode(string discountCode);

        public List<Domain.Entities.Order.Discount> GetAllDiscounts();

        public void UpdateDiscount(Domain.Entities.Order.Discount discount);

        public int AddDiscount(Domain.Entities.Order.Discount discount);

        public Domain.Entities.Order.Discount GetDiscountByDiscountId(int discountId);

        public bool DoesUserUsedDiscount(int userId, int discountId);

        public int AddUserDiscount(Domain.Entities.User.UserDiscounts userDiscounts);

        public void RemoveDiscount(Domain.Entities.Order.Discount discount);

        public void SaveChanges();

    }
}
