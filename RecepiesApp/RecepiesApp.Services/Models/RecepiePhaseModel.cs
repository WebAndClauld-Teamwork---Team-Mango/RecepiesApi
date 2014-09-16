using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RecepiesApp.Services.Models
{
    public class RecepiePhaseModel
    {
        public virtual int Id { get; set; }
        
        public virtual int NumberOfPhase { get; set; }
        
        public virtual string Name { get; set; }
        
        public virtual int Minutes { get; set; }

        //default is False
        public virtual bool IsImportnt { get; set; }
    }
}
