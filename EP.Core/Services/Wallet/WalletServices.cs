using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Interfaces.Wallet;
using EP.Core.ServiceModels.Wallet;
using EP.Domain.Interfaces.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.Wallet
{
    public class WalletServices : IWalletServices
    {
        private readonly IWalletRepository _walletRepository;

        public WalletServices(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public List<WalletViewModel> GetUserWalletViewModel(int userId)
        {

            List<WalletViewModel> result = new List<WalletViewModel>();
            List<Domain.Entities.Wallet.Wallet> wallets = _walletRepository.GetWalletUser(userId);

            

            for (int i = 0;i < wallets.Count();i++)
            {
                WalletViewModel wallet = new WalletViewModel();

                wallet.Amount = wallets[i].Amount;
                wallet.Description = wallets[i].Description;
                wallet.CreateDate = wallets[i].CreateDate;
                wallet.Type = wallets[i].TypeId;

                result.Add(wallet);
            }

            return result;
        }

        public string AddWallet(ChargeWalletServiceModel model)
        {
            
            Domain.Entities.Wallet.Wallet wallet = new Domain.Entities.Wallet.Wallet()
            {
                Description = model.Description,
                CreateDate = DateTime.Now,
                Amount = model.Amount,
                UserId = model.UserId,
                TypeId = model.Type,
                IsPay = model.IsPay
            };

            Domain.Entities.Wallet.Wallet result = null;

            result = _walletRepository.AddWallet(wallet);
            _walletRepository.SaveChanges();

            if (result != null)
            {

                #region Payment

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);

                var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44320/UserPanel/Wallet/OnlinePayment/" + wallet.WalletId);

                if (res.Result.Status == 100)
                {
                    return res.Result.Authority;
                }

                return "";

                #endregion
            }
            else
            {
                return "";
            }
        }

        public long OnlinePayment(OnlinePaymentServiceModel model)
        {

            Domain.Entities.Wallet.Wallet wallet = new Domain.Entities.Wallet.Wallet();

            wallet = _walletRepository.GetWalletByWalletId(model.walletId);

            var payment = new ZarinpalSandbox.Payment(wallet.Amount);
            var res = payment.Verification(model.authority).Result;

            if (res.Status == 100)
            {
                wallet.IsPay = true;

                _walletRepository.UpdateWallet(wallet);
                _walletRepository.SaveChanges();

                return res.RefId;
            }

            return 0;
        }

        public int GetUserBalanceByUserId(int userId)
        {
            return _walletRepository.BalanceUserWallet(userId);
        }
    }
}