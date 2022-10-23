using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.ServiceModels.Account
{
    public class LoginServiceModel
    {

        public bool IsUserExist { get; set; }

        public bool IsPasswordTrue { get; set; }

        public bool IsActive { get; set; }

    }
}
