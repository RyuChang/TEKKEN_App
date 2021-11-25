using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public abstract class BaseService<TEntity, TNameEntity>
        where TEntity : BaseEntity
        where TNameEntity : BaseTranslateName, new()
    {
        protected TekkenDbContext _tekkenDBContext;
        protected DbSet<TEntity> _dataDbSet;
        protected DbSet<TNameEntity> _nameDbSet;
        public string preUrl { get; set; }

        protected string mainTable { get; set; }
        protected string nameTable { get; set; }

        public BaseService(TekkenDbContext tekkenDbContext, DbSet<TEntity> dbset, DbSet<TNameEntity> nameDbSet)
        {
            _tekkenDBContext = tekkenDbContext;
            _dataDbSet = dbset;
            _nameDbSet = nameDbSet;
        }

        public async Task<TEntity> GetEntityByIdAsync(string id)
        {
            return await _dataDbSet.FindAsync(int.Parse(id));
        }

        public async Task<TNameEntity> GetNameEntityByIdAsync(string id)
        {
            return await _nameDbSet.FindAsync(int.Parse(id));
        }

        private bool BaseEntityExistsById(int id)
        {
            return _dataDbSet.Any(e => e.Id == id);
        }

        private bool BaseEntityExistsByCode(int code)
        {
            return _dataDbSet.Any(e => e.Code == code);
        }

        private bool BaseEntityExistsByCode(string code)
        {
            return BaseEntityExistsByCode(int.Parse(code));
        }

        public async Task<List<TEntity>> GetEntities()
        {
            return await _dataDbSet.ToListAsync();
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
            TableCode tableCode = await
            _tekkenDBContext.tableCode.Where(x => x.tableName == this.mainTable).SingleOrDefaultAsync();
            int stateGroupNumber = (stateGroup_code > 0) ? (stateGroup_code - 80000000) * 1000 : 0;

            return tableCode.code + (character_code * 1000) + stateGroupNumber + number;
        }
        public async Task<bool> CreateEntityAsync(TEntity entity)
        {
            await _dataDbSet.AddAsync(entity);
            await _tekkenDBContext.SaveChangesAsync();
            return true;
        }

        #endregion

        #region CreateTranslateName
        public async Task<bool> CreateTranslateNameAllAsync(TNameEntity translateName)
        {
            bool result = false;
            List<Language> languageList = await _tekkenDBContext.language.ToListAsync();

            foreach (Language language in languageList)
            {
                translateName.Language_code = language.code;
                result = await CreateTranslateNameAsync(translateName);
            }
            return result;
        }

        public async Task<bool> CreateTranslateNameAsync(TNameEntity translateName) 
        {

            await _nameDbSet.AddAsync(translateName);
            await _tekkenDBContext.SaveChangesAsync();
            return true;
            //string sql = $"EXECUTE dbo.[TranslateName_GetTranslateNameByCode] " +
            //    $"@tableName={translateName.GetTableName()}, " +
            //    $"@base_code={translateName.Base_code}, " +
            //    $"@name={translateName.Name}, " +
            //    $"@language_code={translateName.Language_code}";

            //var result = await _tekkenDBContext.Database.ExecuteSqlRawAsync(sql);

        }

        //public abstract List<TNameEntity> GetEntity_AllTranslateNamesByCodeAsync(int code);

        public List<TNameEntity> GetAllTranslateNamesByCodeAsync(int code)
        {
            var baseTranslateName = from language in _tekkenDBContext.Set<Language>() //_tekkenDBContext.language
                                    join name in _tekkenDBContext.Set<TNameEntity>().Where(n => n.Base_code == code)
                                        on language.code equals name.Language_code into grouping
                                    from name in grouping.DefaultIfEmpty()
                                    select (new TNameEntity { Id = (name.Id != null) ? name.Id : 0, Base_code = (name.Id != null) ? name.Base_code : 0, Language_code = language.code, Name = name.Name });
            return baseTranslateName.ToList();
        }


        #endregion

        public async Task<BaseEntity> UpdateDataAsync(BaseEntity baseEntity)
        {
            _tekkenDBContext.Entry(baseEntity).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return baseEntity;
        }

        public async Task<BaseTranslateName> UpdateTranslateNameAsync(BaseTranslateName translateName)
        {
            _tekkenDBContext.Entry(translateName).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return translateName;
        }


    }
}