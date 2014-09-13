using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Models
{
    /// <summary>
    /// One to One connected with Web API's User by the Id
    /// </summary>
    class UserInfo
    {
        public UserInfo() 
        {
            this.Recepies = new HashSet<Recepie>();
            this.FavouriteRecepies = new HashSet<UserFavouriteRecepies>();
        }
        
        //TODO: decide on the type of the Id - Guid or int
        public virtual int Id { get; set; }
        
        //should have index
        public virtual string Nickname { get; set; }

        // innerHTML or Plain text
        public virtual string Description { get; set; }
        
        // TODO: - Decide how to store pictures
        // innerHTML or Plain text
        public virtual object Picture { get; set; }
        
        public virtual ICollection<Recepie> Recepies { get; set; }

        public virtual ICollection<UserFavouriteRecepies> FavouriteRecepies { get; set; }

    }
}
