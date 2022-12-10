using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Interfaces.Wallet;
using EP.Core.ServiceModels.UserPanel;
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

        public bool AddWallet(ChargeWalletServiceModel model)
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

            if (wallet != null)
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}