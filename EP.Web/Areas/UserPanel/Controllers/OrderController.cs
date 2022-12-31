using EP.Core.Interfaces.Order;
using EP.Domain.Entities.Order;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Mvc;

namespace EP.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class OrderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public IActionResult Index()
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<Domain.Entities.Order.Order> orders = _orderServices.GetOrdersByUserId(userId);

            return View(orders);
        }

        public IActionResult ShowOrder(int Id, bool? IsDiscount = false)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Domain.Entities.Order.Order order = _orderServices.GetOrderByUserIdAndOrderId(userId, Id);

            ViewBag.IsDiscount = IsDiscount;

            return View(order);
        }

        [System.Web.Mvc.Route("Order/FinalOrder/{orderId}")]
        public IActionResult FinalOrder(int orderId)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            bool result = _orderServices.FinalOrder(userId, orderId);

            if (result)
            {
                return RedirectToAction("ShowOrder", new { Id = orderId });
            }

            return BadRequest();
        }

        [System.Web.Mvc.HttpPost]
        public IActionResult UseDiscount(int orderId, string discountCose)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            bool result = _orderServices.UseDiscount(orderId, userId, discountCose);

            return RedirectToAction("ShowOrder", new { Id = orderId , IsDiscount = result});
        }

    }
}