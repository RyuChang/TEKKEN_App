using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CasCade.Repository
{
    public interface IDropdownService
    {
        List<SelectListItem> ListofCountries();
        List<SelectListItem> ListofStates(int countryId);
        List<SelectListItem> ListofCities(int stateid);
    }
}
