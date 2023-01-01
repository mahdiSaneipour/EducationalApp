using EP.Core.Interfaces.Discount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Discount
{
    public class EditDiscountModel : PageModel
    {
        private readonly IDiscountServices _discountServices;

        public EditDiscountModel(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }

        [BindProperty]
        public Domain.Entities.Order.Discount Discount { get; set; }

        public void OnGet(int discountId)
        {
            Discount = _discountServices.GetDiscountByDiscountId(discountId);
        }

        public IActionResult OnPost(string startDate, string endDate)
        {

            _discountServices.UpdateDiscount(Discount, startDate, endDate);

            return RedirectToPage("Index");
        }
    }
}
