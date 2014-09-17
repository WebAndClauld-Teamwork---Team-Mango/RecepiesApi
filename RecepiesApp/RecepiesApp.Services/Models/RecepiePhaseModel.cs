using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using RecepiesApp.Models;

namespace RecepiesApp.Services.Models
{
    public class RecepiePhaseModel
    {
        
        public static Expression<Func<RecepiePhase, RecepiePhaseModel>> FromDbModel
        {
            get 
            {
                return p => new RecepiePhaseModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsImportnt = p.IsImportnt,
                    Minutes = p.Minutes,
                    NumberOfPhase = p.NumberOfPhase
                };
            }
        }

        public virtual int Id { get; set; }
        
        [Required]
        public virtual int NumberOfPhase { get; set; }
        
        [Required]
        public virtual string Name { get; set; }
        
        [Required]
        public virtual int Minutes { get; set; }

        //default is False
        [Required]
        public virtual bool IsImportnt { get; set; }
    }
}
