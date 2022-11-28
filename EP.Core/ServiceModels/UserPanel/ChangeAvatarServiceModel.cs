using EP.Core.Enums.UserPanel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.ServiceModels.UserPanel
{
    public class ChangeAvatarServiceModel
    {
        [Display(Name = "وضعیت")]
        public ChangeAvatarEnums Status { get; set; }

        [Display(Name = "آدرس آواتار")]
        public string AvatarAddress { get; set; }
    }
}
