using EP.Core.Interfaces.Discount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Discount
{
    public class DeleteDiscountModel : PageModel
    {
        private readonly IDiscountServices _discountServices;

        public DeleteDiscountModel(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }

        [BindProperty]
        public Domain.Entities.Order.Discount Discount { get; set; }

        public void OnGet(int discountId)
        {
            Discount = _discountServices.GetDiscountByDiscountId(discountId);
        }

        public IActionResult OnPost()
        {
            _discountServices.DeleteDiscount(Discount);

            return RedirectToPage("Index");
        }
    }
}
