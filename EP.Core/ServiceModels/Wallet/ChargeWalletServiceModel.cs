using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EP.Core.ServiceModels.Wallet
{
    public class ChargeWalletServiceModel
    {
        public int UserId { get; set; }

        public int Amount { get; set; }

        public int Type { get; set; }

        public bool IsPay { get; set; }

        public string Description { get; set; }
    }
}
