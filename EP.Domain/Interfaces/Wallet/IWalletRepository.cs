using EP.Domain.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Wallet
{
    public interface IWalletRepository
    {
        public void SaveChanges();

        public int BalanceUserWallet(int userId);

        public List<Entities.Wallet.Wallet> GetWalletUser(int userId);

        public Domain.Entities.Wallet.Wallet AddWallet(Domain.Entities.Wallet.Wallet wallet);

        public Domain.Entities.Wallet.Wallet GetWalletByWalletId(int walletId);

        public void UpdateWallet(Domain.Entities.Wallet.Wallet wallet);
    }
}
