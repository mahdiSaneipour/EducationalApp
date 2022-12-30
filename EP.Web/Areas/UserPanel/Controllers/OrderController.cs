using EP.Core.Interfaces.Order;
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
            return View();
        }

        public IActionResult ShowOrder(int Id)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Domain.Entities.Order.Order order = _orderServices.GetOrderByUserIdAndOrderId(userId, Id);
            ViewBag.IsFinaly = false;

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

    }
}