using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IBaseDataService<TDataEntity>
        where TDataEntity : BaseDataEntity, new()
    {
        AppType App { get; }
        string PreUrl { get; set; }

        Task<bool> CreateEntityAsync(TDataEntity entity);
        Task<TDataEntity?> DeleteDataEntityByIdAsync(TDataEntity dataEntity);
        Task<int> GetCreateCode(int number, int character_code = 0, int stateGroup_code = 0);
        Task<int> GetCreateNumber();
        Task<TDataEntity?> GetDataEntityByIdAsync(int id);
        Task<List<TDataEntity>> GetEntities();
        Task<List<TDataEntity>> GetEntitiesByCharacterCode(int characterCode);
        Task<List<TDataEntity>> GetEntitiesWithStateGroup(int stateGroupCode);
        Task<List<SelectListItem>> GetSelectItems();
        Task<BaseDataEntity> UpdateDataAsync(BaseDataEntity BaseDataEntity);
    }
}