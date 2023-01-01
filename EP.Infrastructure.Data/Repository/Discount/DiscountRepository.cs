using EP.Domain.Entities.User;
using EP.Domain.Interfaces.Discount;
using EP.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Discount
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly EPContext _context;

        public DiscountRepository(EPContext context)
        {
            _context = context;
        }

        public int AddDiscount(Domain.Entities.Order.Discount discount)
        {
            _context.Discounts.Add(discount);
            return discount.DiscountId;
        }

        public Domain.Entities.Order.Discount GetDiscountByDiscountCode(string discountCode)
        {
            return _context.Discounts.FirstOrDefault(d => d.DiscountCode == discountCode);
        }

        public Domain.Entities.Order.Discount GetDiscountByDiscountId(int discountId)
        {
            return _context.Discounts.FirstOrDefault(d => d.DiscountId == discountId);
        }

        public void UpdateDiscount(Domain.Entities.Order.Discount discount)
        {
            _context.Discounts.Update(discount);
        }

        public bool DoesUserUsedDiscount(int userId, int discountId)
        {
            return _context.UserDiscounts.Any(us => us.UserId == userId && us.DiscountId == discountId);
        }

        public int AddUserDiscount(UserDiscounts userDiscounts)
        {
            _context.UserDiscounts.Add(userDiscounts);
            return userDiscounts.DiscountId;
        }

        public List<Domain.Entities.Order.Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public void RemoveDiscount(Domain.Entities.Order.Discount discount)
        {
            _context.Discounts.Remove(discount);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
