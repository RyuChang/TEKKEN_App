using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IBaseService<TDataEntity, TNameEntity>
        where TDataEntity : BaseDataEntity
        where TNameEntity : BaseNameEntity, new()
    {
        AppType App { get; }
        string PreUrl { get; set; }

        Task<bool> CreateAllNameEntitiesAsync(TDataEntity dataEntity);
        Task<bool> CreateEntityAsync(TDataEntity entity);
        Task<bool> CreateNameEntityAsync(TNameEntity nameEntity);
        Task<TDataEntity?> DeleteDataEntityByIdAsync(TDataEntity dataEntity);
        Task<int> GetCreateCode(int number, int character_code = 0, int stateGroup_code = 0);
        Task<int> GetCreateNumber();
        Task<TDataEntity?> GetDataEntityByIdAsync(int id);
        Task<List<TDataEntity>> GetEntities();
        Task<List<TDataEntity>> GetEntitiesWithCharacterCode(int characterCode);
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