using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Services.Models
{
    /// <summary>
    /// One to One connected with Web API's User by the Id
    /// </summary>
    public class UserInfoModel
    {
        //public UserInfoModel() 
        //{
        //    this.Recepies = new HashSet<RecepieLightModel>();
        //    this.FavouriteRecepies = new HashSet<RecepieLightModel>();
        //    this.RecepieComments = new HashSet<RecepieCommentModel>();
        //}
        
        public virtual int Id { get; set; }
        
        public virtual string Nickname { get; set; }

        public virtual string Description { get; set; }
        
        public virtual string PictureUrl { get; set; }
        
        public virtual IEnumerable<RecepieLightModel> Recepies { get; set; }

        public virtual IEnumerable<RecepieLightModel> FavouriteRecepies { get; set; }

        public virtual IEnumerable<RecepieCommentModel> RecepieComments { get; set; }
        
    }
}
