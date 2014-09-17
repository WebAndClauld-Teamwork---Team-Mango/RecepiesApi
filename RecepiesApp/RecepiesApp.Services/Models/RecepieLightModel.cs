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
    public class RecepieLightModel
    {
        public static Expression<Func<Recepie, RecepieLightModel>> FromDbModel
        {
            get 
            {
                return rec => new RecepieLightModel()
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Date = rec.Date,
                    Description = rec.Description,
                    PictureUrl = rec.PictureUrl,
                    Nickname = rec.UserInfo.Nickname,
                    UserInfoId = rec.UserInfoId
                };
            }
        }

        private string description;

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string PictureUrl { get; set; }

        public virtual string Description 
        {
            get
            {
                return this.description;
            }
            set 
            {
                this.description = value.Substring(0, 100);
            }
        }
       
        public virtual DateTime Date { get; set; }
        
        public virtual string Nickname { get; set; }

        public virtual int UserInfoId { get; set; }
    }
}
