using System.Collections.Generic;

namespace TekkenApp.Models.Models.Maps
{
    internal class NameMap
    {
        Dictionary<string, string> language { get; set; }
        public NameMap()
        {
            language = new Dictionary<string, string>();
        }
    }
}
