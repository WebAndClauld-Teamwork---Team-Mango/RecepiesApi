using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Services.Models
{
    public class TagModel
    {
        //public TagModel() 
        //{
        //    this.Recepies = new HashSet<RecepieLightModel>();
        //}
        
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        
        
        /// <summary>
        /// Full if Tag viewed by Id. Otherwise null
        /// </summary>
        /// <value>
        /// The recepies.
        /// </value>
        public IEnumerable<RecepieLightModel> Recepies { get; set; }
    }
}
