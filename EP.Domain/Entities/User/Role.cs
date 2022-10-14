using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.User
{
    public class Role
    {

        public Role()
        {

        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name = "اسم رول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string RoleName { get; set; }

        #region Relations

        public virtual List<Role> Roles { get; set; }

        #endregion
    }
}
