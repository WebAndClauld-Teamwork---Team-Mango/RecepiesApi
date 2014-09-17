using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RecepiesApp.Models;

namespace RecepiesApp.Services.Models
{
    public class TagModel
    {
        public static Expression<Func<Tag, TagModel>> FromDbModel
        {
            get 
            {
                return tag => new TagModel()
                {
                    Id = tag.Id,
                    Name = tag.Name
                };
            }
        }
        public static Expression<Func<Tag, TagModel>> FromDbModelWithRecepies
        {
            get 
            {
                return tag => new TagModel()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Recepies = tag.Recepies.Select(RecepieLightModel.FromDbModel.Compile())
                };
            }
        }
        
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
