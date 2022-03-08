using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekkenApp.Models.Models.Maps
{
    internal class StateMap
    {
        public Dictionary<string, string> name { get; set; }
        public StateMap()
        {
            name = new Dictionary<string, string>();
        }
    }
}
