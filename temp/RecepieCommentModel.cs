using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RecepiesApp.Services.Models
{
    public class RecepieCommentModel
    {
        public virtual int Id { get; set; }
        
        public virtual string Content { get; set; }
        
        public virtual int RecepieId { get; set; }
        
        public virtual int UserInfoId { get; set; }

        public virtual string Nickname { get; set; }

        public virtual DateTime Date { get; set; }
    }
}
