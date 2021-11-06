using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface IBaseRepository
    {
        List<BaseModel> GetAllList(BaseType baseType, int? code = null);
        BaseModel GetDetailBaseModelById(int id);

        BaseModel GetRecentBaseModel();
        void SetTable(TableName tableName);
        void Create(BaseModel baseModel);
        void Update(BaseModel baseModel);
        void Delete(int id);

        public List<SelectListItem> GetSelectList(int code = 0);
        public List<SelectListItem> GetSelectListByCharacter(int character_code);
        public List<SelectListItem> GetSelectListByStateGroup(int stateGroup_code, int code = 0);

        //public JsonResult GetJsonSelectListByStateGroup(int stateGroup_code, int code = 0);


        //int SaveOrUpdate(BaseModel baseModel, FormType formType);
    }
}
