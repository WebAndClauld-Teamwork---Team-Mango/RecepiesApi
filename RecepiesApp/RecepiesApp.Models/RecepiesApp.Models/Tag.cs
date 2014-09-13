using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Models
{
    class Tag
    {
        public Tag() 
        {
            this.Recepies = new HashSet<Recepie>();
        }
        
        //TODO: decide on the type of the Id - Guid or int
        public virtual int Id { get; set; }

        //should have index
        //should be unique
        public virtual string Name { get; set; }
        
        public virtual ICollection<Recepie> Recepies { get; set; }
    }
}
