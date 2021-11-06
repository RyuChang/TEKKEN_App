using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface ITekkenVersionRepository
    {
        List<TekkenVersion> GetAllVersions();
        TekkenVersion GetVersion();
        TekkenVersion GetVersionDetail(float version);
        int UpdateVersion(TekkenVersion model, float version);
        int SaveOrUpdate(TekkenVersion model, FormType formType);
        int Remove(float version);
        List<SelectListItem> GetAllVersionsSelectItems();
    }
}
