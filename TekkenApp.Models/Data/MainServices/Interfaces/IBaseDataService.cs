using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IBaseDataService<TDataEntity>
        where TDataEntity : BaseDataEntity, new()
    {

        Task<bool> CreateEntityAsync(TDataEntity entity);
        Task<TDataEntity?> DeleteDataEntityByIdAsync(TDataEntity dataEntity);
        Task<int> GetCreateCode(int number, int? character_code, int? stateGroup_code);
        Task<int> GetCreateNumber();
        Task<TDataEntity?> GetDataEntityByIdAsync(int id);

        Task<TDataEntity?> GetDataEntityByNumberAsync(int id);
        Task<TDataEntity?> GetDataEntityByCharacterCodeAndNumberAsync(int characterCode, int number);
        Task<TDataEntity> GetDataEntityByBaseCodeAsync(int baseCode);

        Task<List<TDataEntity>> GetEntities();
        Task<List<TDataEntity>> GetEntitiesByCharacterCode(int characterCode);
        Task<List<TDataEntity>> GetEntitiesByStateGroup(int stateGroupCode);
        Task<TDataEntity> UpdateDataAsync(TDataEntity BaseDataEntity);
        Task<TDataEntity> GetDataEntityWithAllNameByIdAsync(int id);
        DbSet<TDataEntity> GetDbSet();
    }
}