using EP.Core.Interfaces.Discount;
using EP.Core.Interfaces.Order;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Discount
{
    [PermissionChecker(20)]
    public class IndexModel : PageModel
    {
        private readonly IDiscountServices _discountServices;

        public IndexModel(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }

        public List<Domain.Entities.Order.Discount> Discounts { get; set; }

        public void OnGet()
        {
            Discounts = _discountServices.GetAllDiscounts();
        }
    }
}
