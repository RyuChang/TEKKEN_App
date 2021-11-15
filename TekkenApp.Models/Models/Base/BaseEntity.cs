using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekkenApp.Models
{
    [NotMapped]
    public class BaseEntity
    {

        [Key]
        public int Number { get; set; }
    }
}
