using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using RecepiesApp.Models;

namespace RecepiesApp.Services.Models
{
    public class RecepieCommentModel
    {
        public static Expression<Func<RecepieComment, RecepieCommentModel>> FromDbModel
        {
            get 
            {
                return rc => new RecepieCommentModel()
                {
                    Id = rc.Id,
                    Content = rc.Content,
                    RecepieId = rc.RecepieId,
                    Date = rc.Date,
                    Nickname = rc.UserInfo.Nickname,
                    UserInfoId = rc.UserInfoId
                };
            }
        }

        public virtual int Id { get; set; }
        
        [Required]
        public virtual string Content { get; set; }
        
        [Required]
        public virtual int RecepieId { get; set; }
        
        [Required]
        public virtual int UserInfoId { get; set; }
        
        public virtual string Nickname { get; set; }
        
        [Required]
        public virtual DateTime Date { get; set; }
    }
}
