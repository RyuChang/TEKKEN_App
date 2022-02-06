using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public abstract class BaseService<TDataEntity, TNameEntity> : IBaseService<TDataEntity, TNameEntity> where TDataEntity : BaseDataEntity
        where TNameEntity : BaseNameEntity, new()
    {
        protected TekkenDbContext _tekkenDBContext;

        protected DbSet<TDataEntity> _dataDbSet;
        protected DbSet<TNameEntity> _nameDbSet;
        public string PreUrl { get; set; } = default!;

        public AppType App { get; protected set; }
        protected string MainTable { get; set; } = default!;
        protected string NameTable { get; set; } = default!;

        public BaseService(TekkenDbContext tekkenDbContext, DbSet<TDataEntity> dbset, DbSet<TNameEntity> nameDbSet)
        {
            _tekkenDBContext = tekkenDbContext;
            _dataDbSet = dbset;
            _nameDbSet = nameDbSet;
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

        #region 명칭 데이터
        public async Task<TNameEntity?> GetNameEntityByIdAsync(int id)
        {
            TNameEntity? nameEntity = await _nameDbSet.FindAsync(id);
            return nameEntity;
        }


        #region CreateTranslateName
        public async Task<bool> CreateAllNameEntitiesAsync(TDataEntity dataEntity)
        {
            bool result = false;
            List<Language> languageList = await _tekkenDBContext.language.ToListAsync();

            foreach (Language language in languageList)
            {
                TNameEntity newNameEntity = new TNameEntity();
                newNameEntity.Base_code = dataEntity.Code;
                newNameEntity.Language_code = language.code;
                newNameEntity.Name = dataEntity.Description;

                result = await CreateNameEntityAsync(newNameEntity);
            }
            return result;
        }

        public async Task<bool> CreateNameEntityAsync(TNameEntity nameEntity)
        {

            await _nameDbSet.AddAsync(nameEntity);
            await _tekkenDBContext.SaveChangesAsync();
            return true;
        }
        #endregion

        //public async Task<List<TNameEntity>> GetAllNameEntitiesByCodeAsync(int code)
        //{
        //    var baseTranslateName = from language in _tekkenDBContext.Set<Language>() //_tekkenDBContext.language
        //                            join name in _tekkenDBContext.Set<TNameEntity>().Where(n => n.Base_code == code)
        //                                on language.code equals name.Language_code into grouping
        //                            from name in grouping.DefaultIfEmpty()
        //                            select (new TNameEntity { Id = (name.Id!=null) ? name.Id : 0, Base_code = (name.Id != null) ? name.Base_code : 0, Language_code = language.code, Name = name.Name });

        //    return await baseTranslateName.ToListAsync();
        //}

        public async Task<BaseNameEntity> UpdateNameEntityAsync(BaseNameEntity nameEntity)
        {
            _tekkenDBContext.Entry(nameEntity).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return nameEntity;
        }
        #endregion

        public virtual async Task<List<TDataEntity>> GetEntities()
        {
            return await _dataDbSet.ToListAsync();
        }

        public async Task<List<TDataEntity>> GetEntitiesWithCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(p => p.Character_code == characterCode).ToListAsync();
        }

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

            List<SelectListItem> selectListItems = await (from data in _dataDbSet
                                                          join name in _nameDbSet
                                                              on data.Code equals name.Base_code
                                                          select new SelectListItem { Value = data.Code.ToString(), Text = name.Name.ToString() }).ToListAsync<SelectListItem>();

            selectListItems.Insert(0, new SelectListItem()
            {
                Value = "",
                Text = "---Select---"
            });
            return selectListItems;
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