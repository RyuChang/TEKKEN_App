using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public abstract class BaseNameService<TDataEntity, TNameEntity> : BaseDataService<TDataEntity>, IBaseNameService<TDataEntity, TNameEntity>
                                                                where TNameEntity : BaseNameEntity, new()
                                                                where TDataEntity : BaseDataEntity, new()
    {
        protected DbSet<TNameEntity> _nameDbSet;
        protected string NameTable { get; set; } = default!;

        public BaseNameService(TekkenDbContext tekkenDbContext, DbSet<TDataEntity> dbset, DbSet<TNameEntity> nameDbSet) : base(tekkenDbContext, dbset)
        {
            _tekkenDBContext = tekkenDbContext;
            _dataDbSet = dbset;
            _nameDbSet = nameDbSet;
        }



        #region 명칭 데이터

        public async Task<TNameEntity> GetNameEntitiyByBaseCodeAndLanguageCode(int baseCode, string languageCode)
        {
            return await _nameDbSet.Where(n => n.Base_code == baseCode).Where(n => n.Language_code == languageCode).FirstOrDefaultAsync();
        }


        public async Task<TNameEntity?> GetNameEntityByIdAsync(int id)
        {
            TNameEntity? nameEntity = await _nameDbSet.FindAsync(id);
            return nameEntity;
        }


        #region CreateTranslateName
        public async Task<bool> CreateAllNameEntitiesAsync(TDataEntity dataEntity)
        {
            bool result = false;
            List<Language> languageList = await _tekkenDBContext.Language.ToListAsync();

            foreach (Language language in languageList)
            {
                result = await CreateNameEntityAsync(dataEntity, language.Language_code);
            }
            return result;
        }

        public async Task<bool> CreateNameEntityAsync(TDataEntity dataEntity, string language_code)
        {
            TNameEntity newNameEntity = new TNameEntity();
            newNameEntity.Base_code = dataEntity.Code;
            newNameEntity.Language_code = language_code;
            newNameEntity.Name = dataEntity.Description;

            await CreateNameEntityAsync(newNameEntity);
            return true;
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



        #region Load Entity by Character

        #endregion
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

        public async Task<int> GetCreateNumberByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(d => d.Character_code == characterCode).MaxAsync(p => (int?)p.Number + 1) ?? 1;
        }
        public async Task<int> GetCreateNumberByStateGroup(int stateGroupCode)
        {
            return await _dataDbSet.Where(d => d.StateGroup_code == stateGroupCode).MaxAsync(p => (int?)p.Number + 1) ?? 1;
        }
        #endregion
    }
}