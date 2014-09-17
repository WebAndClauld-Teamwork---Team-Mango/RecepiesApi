using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Models
{
    public class Tag : MarkableAsDeleted
    {
        private ICollection<Recepie> recepies;

        public Tag() 
        {
            this.recepies = new HashSet<Recepie>();
        }
        
        public virtual int Id { get; set; }

        [Required]
        [Index(IsUnique=true)]
        [MaxLength(50)]
        public virtual string Name { get; set; }

        public virtual ICollection<Recepie> Recepies 
        {
            get { return this.recepies; } 
            set { this.recepies = value; } 
        }
        
        [Index]
        [Required]
        public override bool IsDeleted { get; set; }
    }
}
