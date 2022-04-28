using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public abstract class BaseDataService<TDataEntity> : IBaseDataService<TDataEntity> where TDataEntity : BaseDataEntity, new()
    {
        protected TekkenDbContext _tekkenDBContext;

        protected DbSet<TDataEntity> _dataDbSet;

        public string PreUrl { get; set; } = default!;

        public AppType App { get; protected set; }
        protected string MainTable { get; set; } = default!;

        protected string language_code = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        public BaseDataService(TekkenDbContext tekkenDbContext, DbSet<TDataEntity> dbset)
        {
            _tekkenDBContext = tekkenDbContext;
            _dataDbSet = dbset;
        }

        #region 메인 데이터

        public async Task<TDataEntity?> GetDataEntityByIdAsync(int id)
        {
            return await _dataDbSet.FindAsync(id);
        }
        public async Task<TDataEntity?> GetDataEntityByNumberAsync(int number)
        {
            return await _dataDbSet.Where(data => data.Number == number).FirstOrDefaultAsync();
        }

        public async Task<TDataEntity?> GetDataEntityByCharacterCodeAndNumberAsync(int characterCode, int number)
        {
            return await _dataDbSet.Where(data => data.Character_code == characterCode && data.Number == number).FirstOrDefaultAsync();
        }
        public async Task<TDataEntity> GetDataEntityByBaseCodeAsync(int baseCode)
        {
            return await _dataDbSet.Where(d => d.Base_code == baseCode).FirstOrDefaultAsync();
        }

        public async Task<TDataEntity> GetDataEntityWithAllNameByIdAsync(int id)
        {
            return await _dataDbSet.Where(d => d.Id == id).Include(d => d.NameSet).FirstOrDefaultAsync();
        }

        private bool BaseDataEntityExistsById(int id)
        {
            return _dataDbSet.Any(e => e.Id == id);
        }

        private bool BaseDataEntityExistsByCode(int code)
        {
            return _dataDbSet.Any(e => e.Code == code);
        }

        private bool BaseDataEntityExistsByCode(string code)
        {
            return BaseDataEntityExistsByCode(int.Parse(code));
        }
        #region 메인 데이터 삭제
        public async Task<TDataEntity?> DeleteDataEntityByIdAsync(TDataEntity dataEntity)
        {

            if (dataEntity != null)
            {

                _dataDbSet.Remove(dataEntity);
                await _tekkenDBContext.SaveChangesAsync();
            }
            //GetDataEntityByIdAsync
            //_dataDbSet.Remove(id);
            //return await _dataDbSet.FindAsync(id);
            return dataEntity;
        }

        #endregion

        #endregion


        public virtual async Task<List<TDataEntity>> GetEntities()
        {
            return await _dataDbSet.ToListAsync();
        }

        #region Load Entity by Character
        public async Task<List<TDataEntity>> GetEntitiesByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(p => p.Character_code == characterCode).Include(p => p.NameSet).ToListAsync();
        }
        #endregion

        public async Task<List<TDataEntity>> GetEntitiesByStateGroup(int stateGroupCode)
        {
            return await _dataDbSet.Where(p => p.StateGroup_code == stateGroupCode || stateGroupCode == 0).Include(p => p.NameSet).ToListAsync();
        }

        #region GetCreateNumber
        public async Task<int> GetCreateNumber()
        {
            return await _dataDbSet.MaxAsync(p => (int?)p.Number + 1) ?? 1;
        }

        public async Task<int> GetCreateNumberByStateGroupCode(int stateGroupCode)
        {
            return await _dataDbSet.Where(p => p.StateGroup_code == stateGroupCode).MaxAsync(p => (int?)p.Number + 1) ?? 1;
        }

        public async Task<int> GetCreateNumberByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(p => p.Character_code == characterCode).MaxAsync(p => (int?)p.Number + 1) ?? 1;
        }
        #endregion

        #region GetCreateCode
        public async Task<int> GetCreateCode(int number, int? character_code, int? stateGroup_code)
        {
            TableCode? tableCode = await
            _tekkenDBContext.tableCode.Where(x => x.tableName == this.MainTable).SingleOrDefaultAsync();

            int characterCodeNumber = (character_code is not null) ? character_code.Value * 1000 : 0;
            int stateGroupNumber = (stateGroup_code is not null) ? (stateGroup_code.Value - 80000000) * 1000 : 0;

            int code = tableCode is not null ? tableCode.code + characterCodeNumber + stateGroupNumber + number : 0;
            return code;
        }
        public async Task<bool> CreateEntityAsync(TDataEntity entity)
        {
            await _dataDbSet.AddAsync(entity);
            await _tekkenDBContext.SaveChangesAsync();
            return true;
        }

        #endregion


        public async Task<TDataEntity> UpdateDataAsync(TDataEntity BaseDataEntity)
        {
            _tekkenDBContext.Entry(BaseDataEntity).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return BaseDataEntity;
        }


    }
}