using EP.Domain.Interfaces.Wallet;
using EP.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Wallet
{
    public class WalletRepository : IWalletRepository
    {
        EPContext _context;

        public WalletRepository(EPContext context)
        {
            _context = context;
        }

        public int BalanceUserWallet(int userId)
        {
            var deposit = _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay == true)
                .Select(w => w.Amount).ToList();

            var withdraw = _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 2 && w.IsPay == true)
                .Select(w => w.Amount).ToList();

            int result = deposit.Sum() - withdraw.Sum();

            return result;
        }

        public List<Domain.Entities.Wallet.Wallet> GetWalletUser(int userId)
        {
            return _context.Wallets.Where(w => w.UserId == userId && w.IsPay == true).ToList();
        }

        public Domain.Entities.Wallet.Wallet AddWallet(Domain.Entities.Wallet.Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            Console.WriteLine("walletId : " + wallet.WalletId);
            return wallet;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
