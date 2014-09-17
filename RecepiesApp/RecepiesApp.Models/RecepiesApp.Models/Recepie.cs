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
        private ICollection<RecepiePhase> phases;
        private ICollection<RecepieComment> comments;
        private ICollection<Tag> tags;
        private ICollection<UserFavouriteRecepie> usersFavouritedThisRecepie;

        public Recepie() 
        {
            this.tags = new HashSet<Tag>();
            this.comments = new HashSet<RecepieComment>();
            this.phases = new HashSet<RecepiePhase>();
            this.usersFavouritedThisRecepie = new HashSet<UserFavouriteRecepie>();
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
        
        public virtual ICollection<Tag> Tags
        { 
            get 
            {
                return this.tags;
            } 
            set 
            {
                this.tags = value;
            }
        }
        
        public virtual ICollection<RecepieComment> Comments
        { 
            get 
            {
                return this.comments;
            } 
            set 
            {
                this.comments = value;
            }
        }
        
        public virtual ICollection<RecepiePhase> Phases 
        { 
            get 
            {
                return this.phases;
            } 
            set 
            {
                this.phases = value;
            }
        }

        public virtual ICollection<UserFavouriteRecepie> UsersFavouritedThisRecepie
        { 
            get 
            {
                return this.usersFavouritedThisRecepie;
            } 
            set 
            {
                this.usersFavouritedThisRecepie = value;
            }
        }
        
        [Index]
        [Required]
        public override bool IsDeleted { get; set; }
    }
}
