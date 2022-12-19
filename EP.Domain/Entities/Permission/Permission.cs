using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Permission
{
    public class Permission
    {

        public Permission()
        {

        }

        [Key]
        public int PermissionId { get; set; }

        [Display(Name = "نام دسترسی")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public string PermissionName { get; set; }

        [Display(Name = "زیر مجموعه")]
        public int? ParentId { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public virtual List<Permission> Parent { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
