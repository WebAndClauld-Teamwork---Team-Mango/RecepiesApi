using System.ComponentModel.DataAnnotations;
namespace RecepiesApp.Services.Models
{
    public class UserInfoLoggedModel
    {
        [Required]
        public virtual string Nickname { get; set; }

        [Required]
        public virtual string SessionKey { get; set; }
    }
}