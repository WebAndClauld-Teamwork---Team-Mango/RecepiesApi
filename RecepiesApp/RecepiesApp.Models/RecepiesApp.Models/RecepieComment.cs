using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RecepiesApp.Models
{
    public class RecepieComment : MarkableAsDeleted
    {
        public virtual int Id { get; set; }
        
        [Required]
        public virtual string Content { get; set; }

        [Index]
        [Required]
        public virtual DateTime Date { get; set; }
        
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
        public override bool IsDeleted { get; set; }
    }
}
