using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public interface ITranslateNameRepository
    {
        TranslateName GetTranslateNameByCode(int code, TableName table);
        TranslateName GetTranslateNameById(int id, TableName tableName);
        
        List<TranslateName> GetAllTranslateNamesByCode(int code);

        BaseModel GetBaseModelById(int id);

        void SetTable(TableName tableName, TableName subTableName = TableName.NONE);

        void Update(TranslateName translateName);

        void Create(TranslateName translateName);
        
        void Merge(TranslateName translateName);

    }
}
