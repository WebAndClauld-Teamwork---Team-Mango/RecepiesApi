using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RecepiesApp.Models
{
    public class RecepiePhase
    {
        public virtual int Id { get; set; }
        
        [Required]
        public virtual int NumberOfPhase { get; set; }
        
        [Required]
        [MaxLength(50)]
        public virtual string Name { get; set; }
        
        [Required]
        public virtual int Minutes { get; set; }
        
        [Index]
        [Required]
        public virtual int RecepieId { get; set; }

        public virtual Recepie Recepie { get; set; }
        
        //default is False
        [Required]
        public virtual bool IsImportnt { get; set; }
        
        [Index]
        [Required]
        public virtual bool IsDeleted { get; set; }
    }
}
