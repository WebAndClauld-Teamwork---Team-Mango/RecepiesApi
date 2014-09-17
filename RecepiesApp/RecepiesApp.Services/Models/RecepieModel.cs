using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.Services.Models
{
    public class RecepieModel
    {
        //public RecepieModel() 
        //{
        //    this.Tags = new HashSet<TagModel>();
        //    this.Comments = new HashSet<RecepieCommentModel>();
        //    this.RecepiePhases = new HashSet<RecepiePhaseModel>();
        //    this.UsersFavouritedThisRecepie = new HashSet<UserInfoLightModel>();
        //}
        
        public virtual int Id { get; set; }
        
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
        
        public virtual string PictureUrl { get; set; }
       
        public virtual DateTime Date { get; set; }
        
        public virtual UserInfoLightModel UserInfo { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }
        
        public IEnumerable<RecepieCommentModel> Comments { get; set; }

        public IEnumerable<RecepiePhaseModel> RecepiePhases { get; set; }

        public IEnumerable<UserInfoLightModel> UsersFavouritedThisRecepie { get; set; }
    }
}
