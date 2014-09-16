using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecepiesApp.Models
{
    public abstract class MarkableAsDeleted
    {
        public abstract bool IsDeleted { get; set; }
    }
}