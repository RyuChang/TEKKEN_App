using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IBaseStateGroupRepository : IBaseRepository
    {
        BaseModel GetRecentBaseModel(int character_code);
        //protected int SaveOrUpdate(BaseModel baseModel, FormType formType);

    }
}
