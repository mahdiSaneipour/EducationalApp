using EP.Core.Interfaces.Discount;
using EP.Core.Tools.ConvertNumber;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Discount
{
    public class CreateDiscountModel : PageModel
    {
        private readonly IDiscountServices _discountServices;

        public CreateDiscountModel(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }

        [BindProperty]
        public Domain.Entities.Order.Discount Discount { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string startDate, string endDate)
        {

            int result = _discountServices.AddDiscount(Discount, startDate, endDate);

            if (result == 0)
            {
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
