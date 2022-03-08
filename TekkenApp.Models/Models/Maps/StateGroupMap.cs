using System.Collections.Generic;

namespace TekkenApp.Models.Models.Maps
{
    internal class StateGroupMap
    {
        public Dictionary<string, string> name { get; set; }
        public StateGroupMap()
        {
            name = new Dictionary<string, string>();
        }
    }
}
