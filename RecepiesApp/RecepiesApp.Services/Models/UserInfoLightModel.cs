using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RecepiesApp.Models;

namespace RecepiesApp.Services.Models
{
    public class UserInfoLightModel
    {
        public static Expression<Func<UserInfo, UserInfoLightModel>> FromDbModel
        {
            get 
            {
                return user => new UserInfoLightModel()
                {
                    Id = user.Id,
                    Nickname = user.Description,
                    PictureUrl = user.PictureUrl
                };
            }
        }

        public virtual int Id { get; set; }
        
        public virtual string Nickname { get; set; }
        
        public virtual string PictureUrl { get; set; }
        
    }
}
