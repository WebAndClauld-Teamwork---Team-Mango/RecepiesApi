using System;
using System.Collections.Generic;
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
        }
        
        //TODO: decide on the type of the Id - Guid or int
        public virtual int Id { get; set; }

        // innerHTML or Plain text
        public virtual string Description { get; set; }
        
        // TODO: - Decide how to store pictures
        // innerHTML or Plain text
        public virtual object Picture { get; set; }
        
        //should have index
        public virtual DateTime Date { get; set; }
        
        public virtual UserInfo UserInfoBy { get; set; }
        
        //should have index
        public virtual int UserInfoId { get; set; }
        
        //should have index
        public virtual ICollection<Tag> Tags { get; set; }
        
        public virtual ICollection<RecepieComment> CommentsOnRecepies { get; set; }
    }
}
