using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Models
{
    public class Recepie
    {
        public Recepie() 
        {
            this.Tags = new HashSet<Tag>();
            this.CommentsOnRecepies = new HashSet<RecepieComment>();
            this.RecepiePhases = new HashSet<RecepiePhase>();
        }
        
        public virtual int Id { get; set; }
        
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
        
        public virtual ICollection<Tag> Tags { get; set; }
        
        public virtual ICollection<RecepieComment> CommentsOnRecepies { get; set; }

        public virtual ICollection<RecepiePhase> RecepiePhases { get; set; }
        
        [Index]
        [Required]
        public virtual bool IsDeleted { get; set; }
    }
}
