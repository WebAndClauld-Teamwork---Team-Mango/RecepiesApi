using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Models
{
    /// <summary>
    /// One to One connected with Web API's User by the Id
    /// </summary>
    public class UserInfo
    {
        public UserInfo() 
        {
            this.Recepies = new HashSet<Recepie>();
            this.FavouriteRecepies = new HashSet<UserFavouriteRecepie>();
            this.RecepieComments = new HashSet<RecepieComment>();
        }
        
        public virtual int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        [Index(IsUnique=true)]
        public virtual string Nickname { get; set; }

        public virtual string Description { get; set; }
        
        public virtual string PictureUrl { get; set; }
        
        public virtual ICollection<Recepie> Recepies { get; set; }

        public virtual ICollection<UserFavouriteRecepie> FavouriteRecepies { get; set; }

        public virtual ICollection<RecepieComment> RecepieComments { get; set; }
        
        [Index]
        [Required]
        public virtual bool IsDeleted { get; set; }
    }
}
