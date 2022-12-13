using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Interfaces.Wallet;
using EP.Core.ServiceModels.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EP.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class WalletController : Controller
    {

        private readonly IWalletServices _walletServices;

        public WalletController(IWalletServices walletServices)
        {
            _walletServices = walletServices;
        }

        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<WalletViewModel> wallets = _walletServices.GetUserWalletViewModel(userId);

            ViewData["wallets"] = wallets;

            return View("Wallet");
        }


        [HttpPost]
        [Route("UserPanel/Wallet")]
        public IActionResult Index(ChargeWalletViewModel model)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!ModelState.IsValid)
            {
                List<WalletViewModel> wallets = _walletServices.GetUserWalletViewModel(userId);

                ViewData["wallets"] = wallets;
                return View("Wallet",model);
            }

            string result = "";

            ChargeWalletServiceModel wallet = new ChargeWalletServiceModel();

            wallet.Description = "شارژ حساب";
            wallet.Amount = model.Amount;
            wallet.UserId = userId;
            wallet.IsPay = false;
            wallet.Type = 1;

            result = _walletServices.AddWallet(wallet);

            if (result == "" || result == null)
            {
                List<WalletViewModel> wallets = _walletServices.GetUserWalletViewModel(userId);

                ViewData["wallets"] = wallets;
                ModelState.AddModelError("Amount","خطایی در سیستم رخ داده است");
                return View("Wallet",model);
            }

            return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result);
        }

        [Route("UserPanel/Wallet/OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int Id)
        {

            if (HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {

                string authority = HttpContext.Request.Query["Authority"];

                OnlinePaymentServiceModel payment = new OnlinePaymentServiceModel()
                {
                    walletId = Id,
                    authority = authority
                };

                long result = _walletServices.OnlinePayment(payment);

                ViewBag.code = result;
                ViewBag.isSuccess = false;

                if (result != 0)
                {
                    ViewBag.isSuccess = true;
                }

            } else
            {
                ViewBag.isSuccess = false;
            }

            return View();
        }
    }
}
