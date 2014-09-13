using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecepiesApp.Models
{
    class RecepieComment
    {
        //TODO: decide on the type of the Id - Guid or int
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        
        //should have index
        public virtual int RecepieId { get; set; }
        public virtual Recepie Recepie { get; set; }

        //should have index
        public virtual int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
