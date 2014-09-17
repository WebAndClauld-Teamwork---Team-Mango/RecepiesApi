using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Models
{
    public class Recepie : MarkableAsDeleted
    {
        public Recepie() 
        {
            this.Tags = new HashSet<Tag>();
            this.Comments = new HashSet<RecepieComment>();
            this.Phases = new HashSet<RecepiePhase>();
            this.UsersFavouritedThisRecepie = new HashSet<UserFavouriteRecepie>();
        }
        
        public virtual int Id { get; set; }
        
        [Required]
        public virtual string Name { get; set; }
        
        [Required]
        public virtual string Description { get; set; }
        
        public virtual string PictureUrl { get; set; }
        
        [Index]
        [Required]
        public virtual DateTime Date { get; set; }
        
        [Index]
        [Required]
        public virtual int UserInfoId { get; set; }

        public virtual UserInfo UserInfo { get; set; }
        
        public /*virtual*/ ICollection<Tag> Tags { get; set; }
        
        public /*virtual*/ ICollection<RecepieComment> Comments { get; set; }
        
        public /*virtual*/ ICollection<RecepiePhase> Phases { get; set; }

        public /*virtual*/ ICollection<UserFavouriteRecepie> UsersFavouritedThisRecepie { get; set; }
        
        [Index]
        [Required]
        public override bool IsDeleted { get; set; }
    }
}
