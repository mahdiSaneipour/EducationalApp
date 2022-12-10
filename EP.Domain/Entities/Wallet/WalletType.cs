using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Wallet
{
    public class WalletType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string TypeTitle { get; set; }

        #region Relations

        public ICollection<Wallet> Wallets { get; set; }

        #endregion

    }
}
