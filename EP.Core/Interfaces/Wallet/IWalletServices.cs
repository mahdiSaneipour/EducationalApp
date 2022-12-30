using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.ServiceModels.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Wallet
{
    public interface IWalletServices
    {
        public List<WalletViewModel> GetUserWalletViewModel(int userId);

        public string AddWallet(ChargeWalletServiceModel model);

        public long OnlinePayment(OnlinePaymentServiceModel model);

        public int GetUserBalanceByUserId(int userId);
    }
}
