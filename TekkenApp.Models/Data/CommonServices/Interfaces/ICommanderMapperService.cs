using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekkenApp.Data
{
    public interface ICommanderMapperService
    {
        string MapKey(string formedKey);
        string MapState(string stateCode, string languageCode);
    }
}
