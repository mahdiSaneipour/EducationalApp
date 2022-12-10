using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.ServiceModels.UserPanel;
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

        public bool AddWallet(ChargeWalletServiceModel model);
    }
}
