using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Services.Models
{
    /// <summary>
    /// One to One connected with Web API's User by the Id
    /// </summary>
    public class UserInfoLightModel
    {
        public virtual int Id { get; set; }
        
        public virtual string Nickname { get; set; }
        
        public virtual string PictureUrl { get; set; }
        
    }
}
