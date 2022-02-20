using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public abstract class BaseDataService<TDataEntity> : IBaseDataService<TDataEntity> where TDataEntity : BaseDataEntity
        , new()
    {
        protected TekkenDbContext _tekkenDBContext;

        protected DbSet<TDataEntity> _dataDbSet;

        public string PreUrl { get; set; } = default!;

        public AppType App { get; protected set; }
        protected string MainTable { get; set; } = default!;

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
            return await _dataDbSet.Where(p => p.Character_code == characterCode).ToListAsync();
        }
        #endregion

        public async Task<List<TDataEntity>> GetEntitiesWithStateGroup(int stateGroupCode)
        {
            return await _dataDbSet.Where(p => p.StateGroup_code == stateGroupCode).ToListAsync();
        }

        public async Task<List<TDataEntity>> GetEntitiesWithName()
        {
            return await _dataDbSet.Include("NameSet").ToListAsync();
        }

        public List<TDataEntity> GetEntitiesWithName(string tname)
        {
            return _dataDbSet.Include(tname).ToList();
        }
        public List<TDataEntity> GetEntitiesWithNameByStateGroup(int stateGroupCode)
        {
            return _dataDbSet.Where(d => d.StateGroup_code == stateGroupCode).Include("NameSet").ToList();
        }

        public async Task<List<SelectListItem>> GetSelectItems()
        {
            throw new NotImplementedException();
/*            List<SelectListItem> selectListItems = await (from data in _dataDbSet
                                                          join name in _nameDbSet
                                                              on data.Code equals name.Base_code
                                                          select new SelectListItem { Value = data.Code.ToString(), Text = name.Name.ToString() }).ToListAsync<SelectListItem>();

            selectListItems.Insert(0, new SelectListItem()
            {
                Value = "",
                Text = "---Select---"
            });
            return selectListItems;*/
        }

        #region GetCreateNumber
        public async Task<int> GetCreateNumber()
        {
            return await _dataDbSet.MaxAsync(p => (int?)p.Number + 1) ?? 1;
        }
        #endregion

        #region GetCreateCode
        public async Task<int> GetCreateCode(int number, int character_code = 0, int stateGroup_code = 0)
        {
            TableCode? tableCode = await
            _tekkenDBContext.tableCode.Where(x => x.tableName == this.MainTable).SingleOrDefaultAsync();
            int stateGroupNumber = (stateGroup_code > 0) ? (stateGroup_code - 80000000) * 1000 : 0;

            int code = tableCode is not null ? tableCode.code + (character_code * 1000) + stateGroupNumber + number : 0;
            return code;
        }
        public async Task<bool> CreateEntityAsync(TDataEntity entity)
        {
            await _dataDbSet.AddAsync(entity);
            await _tekkenDBContext.SaveChangesAsync();
            return true;
        }

        #endregion


        public async Task<BaseDataEntity> UpdateDataAsync(BaseDataEntity BaseDataEntity)
        {
            _tekkenDBContext.Entry(BaseDataEntity).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return BaseDataEntity;
        }


    }

}