using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Services.Models
{
    public class RecepieLightModel
    {
        private string description;

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string PictureUrl { get; set; }

        public virtual string Description 
        {
            get
            {
                return this.description;
            }
            set 
            {
                this.description = value.Substring(0, 100);
            }
        }
       
        public virtual DateTime Date { get; set; }
        
        public virtual string Nickname { get; set; }
    }
}
