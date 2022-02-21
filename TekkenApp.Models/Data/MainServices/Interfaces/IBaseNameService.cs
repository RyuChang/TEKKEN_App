using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IBaseNameService<TDataEntity, TNameEntity>
        where TDataEntity : BaseDataEntity
        where TNameEntity : BaseNameEntity, new()
    {
        AppType App { get; }
        string PreUrl { get; set; }

        Task<bool> CreateAllNameEntitiesAsync(TDataEntity dataEntity);
        Task<bool> CreateEntityAsync(TDataEntity entity);
        Task<bool> CreateNameEntityAsync(TNameEntity nameEntity);
        Task<bool> CreateNameEntityAsync(TDataEntity dataEntity, string language_code);
        Task<TDataEntity?> DeleteDataEntityByIdAsync(TDataEntity dataEntity);
        Task<int> GetCreateCode(int number, int character_code = 0, int stateGroup_code = 0);
        Task<int> GetCreateNumber();
        Task<int> GetCreateNumberByCharacterCode(int characterCode);
        Task<int> GetCreateNumberByStateGroup(int stateGroupCode);

        Task<TDataEntity?> GetDataEntityByIdAsync(int id);
        Task<List<TDataEntity>> GetEntities();
        Task<TNameEntity> GetNameEntitiyByBaseCodeAndLanguageCode(int baseCode, string languageCode);
        Task<List<TDataEntity>> GetEntitiesByCharacterCode(int characterCode);
        Task<List<TDataEntity>> GetEntitiesWithName();
        List<TDataEntity> GetEntitiesWithName(string tname);
        List<TDataEntity> GetEntitiesWithNameByStateGroup(int stateGroupCode);
        Task<List<TDataEntity>> GetEntitiesWithStateGroup(int stateGroupCode);
        Task<TNameEntity?> GetNameEntityByIdAsync(int id);
        Task<List<SelectListItem>> GetSelectItems();
        Task<BaseDataEntity> UpdateDataAsync(BaseDataEntity BaseDataEntity);
        Task<BaseNameEntity> UpdateNameEntityAsync(BaseNameEntity nameEntity);
    }
}