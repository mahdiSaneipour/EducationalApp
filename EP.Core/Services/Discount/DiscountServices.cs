using EP.Core.Interfaces.Discount;
using EP.Domain.Interfaces.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.Discount
{
    public class DiscountServices : IDiscountServices
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountServices(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public List<Domain.Entities.Order.Discount> GetAllDiscounts()
        {
            return _discountRepository.GetAllDiscounts();
        }
    }
}
