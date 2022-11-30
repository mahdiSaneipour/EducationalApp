using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.JsonModel.UserPanel
{
    public class ResultUploadAvatarJsonModel
    {
        [Display(Name = "آدرس اواتار")]
        public string avatarAddress { get; set; }

        [Display(Name = "وضعیت")]
        public string status { get; set; }
    }
}
