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
        public virtual int Id { get; set; }
        
        public virtual string Nickname { get; set; }

        public virtual string Description { get; set; }
        
        public virtual string PictureUrl { get; set; }
        
        public IEnumerable<RecepieLightModel> Recepies { get; set; }

        public IEnumerable<RecepieLightModel> FavouriteRecepies { get; set; }

        public IEnumerable<RecepieCommentModel> RecepieComments { get; set; }
        
    }
}
