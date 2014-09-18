using System.ComponentModel.DataAnnotations;
namespace RecepiesApp.Services.Models
{
    public class UserInfoLoginModel
    {
        [Required]
        public virtual string Nickname { get; set; }
        
        [Required]
        public virtual string AuthCode { get; set; }
    }
}