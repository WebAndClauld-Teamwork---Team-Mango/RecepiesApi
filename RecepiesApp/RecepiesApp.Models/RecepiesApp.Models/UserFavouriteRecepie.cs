using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Models
{
    public class UserFavouriteRecepie
    {
        public virtual int Id { get; set; }
        
        [Index]
        [Required]
        public virtual int RecepieId { get; set; }

        public virtual Recepie Recepie { get; set; }
        
        [Index]
        [Required]
        public virtual int UserInfoId { get; set; }

        public virtual UserInfo UserInfo { get; set; }
        
        [Index]
        [Required]
        public virtual bool IsDeleted { get; set; }
    }
}
