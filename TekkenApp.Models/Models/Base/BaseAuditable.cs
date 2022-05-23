using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekkenApp.Models
{
    public abstract class BaseAuditable
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        //public DateTimeOffset? DateDeleted { get; set; }
    }
}
