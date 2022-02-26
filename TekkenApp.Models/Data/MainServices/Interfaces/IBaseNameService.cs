using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IBaseNameService<TDataEntity, TNameEntity> : IBaseDataService<TDataEntity>
                                                                    where TDataEntity : BaseDataEntity, new()
                                                                    where TNameEntity : BaseNameEntity, new()
    {
        AppType App { get; }
        string PreUrl { get; set; }

        Task<bool> CreateAllNameEntitiesAsync(TDataEntity dataEntity);
        Task<bool> CreateNameEntityAsync(TNameEntity nameEntity);
        Task<bool> CreateNameEntityAsync(TDataEntity dataEntity, string language_code);
        Task<int> GetCreateNumberByCharacterCode(int characterCode);
        Task<int> GetCreateNumberByStateGroup(int stateGroupCode);

        Task<TNameEntity> GetNameEntitiyByBaseCodeAndLanguageCode(int baseCode, string languageCode);
        Task<List<TDataEntity>> GetEntitiesWithAllNames();
        Task<List<TDataEntity>> GetEntitiesWithName();
        List<TDataEntity> GetEntitiesWithNameByStateGroup(int stateGroupCode);
        Task<TNameEntity?> GetNameEntityByIdAsync(int id);
        Task<List<SelectListItem>> GetSelectItems();
        Task<BaseNameEntity> UpdateNameEntityAsync(BaseNameEntity nameEntity);
    }
}