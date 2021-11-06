using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IBaseDefaultRepository : IBaseRepository
    {

   
        BaseModel GetRecentBaseModel(int character_code);
   
    }
}
