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
    public class UserInfo : MarkableAsDeleted
    {
        private ICollection<Recepie> recepies;
        private ICollection<UserFavouriteRecepie> favouriteRecepies;
        private ICollection<RecepieComment> recepieComments;

        public UserInfo() 
        {
            this.recepies = new HashSet<Recepie>();
            this.favouriteRecepies = new HashSet<UserFavouriteRecepie>();
            this.recepieComments = new HashSet<RecepieComment>();
        }
        
        public virtual int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        [Index(IsUnique=true)]
        public virtual string Nickname { get; set; }

        [Required]
        public virtual string AuthCode { get; set; }

        public virtual string SessionKey { get; set; }

        public virtual DateTime SessionExpirationDate { get; set; }

        public virtual string Description { get; set; }
        
        public virtual string PictureUrl { get; set; }

        public virtual ICollection<Recepie> Recepies
        { 
            get 
            {
                return this.recepies;
            } 
            set 
            {
                this.recepies = value;
            }
        }

        public virtual ICollection<UserFavouriteRecepie> FavouriteRecepies
        { 
            get 
            {
                return this.favouriteRecepies;
            } 
            set 
            {
                this.favouriteRecepies = value;
            }
        }

        public virtual ICollection<RecepieComment> RecepieComments 
        { 
            get 
            {
                return this.recepieComments;
            } 
            set 
            {
                this.recepieComments = value;
            }
        }
        
        [Index]
        [Required]
        public override bool IsDeleted { get; set; }
    }
}
