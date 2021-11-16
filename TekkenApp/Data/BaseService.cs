using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public abstract class BaseService<TEntity, TNameEntity>
        where TEntity : BaseEntity
        where TNameEntity : BaseTranslateName
    {
        protected TekkenDbContext _tekkenDBContext;
        protected string mainTable { get; set; }
        protected string nameTable { get; set; }
        protected DbSet<TEntity> dataDbSet;
        protected DbSet<TNameEntity> nameDbSet;

        public BaseService(TekkenDbContext tekkenDbContext, DbSet<TNameEntity> dbset)
        {

            _tekkenDBContext = tekkenDbContext;
        }


        public async Task<List<TEntity>> GetEntities()
        {
            return await dataDbSet.ToListAsync();
        }

        #region GetRecentBaseModel
        public async Task<BaseUtil> GetRecentBaseModel(TableName tableName, int character_code = 0)
        {
            string sql = $"SELECT ISNULL(MAX(number), 0) + 1 AS Number " +
                       $"FROM [TEKKEN].[dbo].[{tableName}] where 1 = 1";
            BaseUtil result = await _tekkenDBContext.BaseUtil.FromSqlRaw<BaseUtil>(sql).FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region GetCreateNumber
        public async Task<int> GetCreateNumber(TableName tableName)
        {
            BaseUtil result = await GetRecentBaseModel(tableName);
            return result.Number;
        }
        #endregion

        #region GetCreateCode
        public async Task<int> GetCreateCode(TableName tableName, int number, int character_code, int stateGroup_code)
        {
            TableCode tableCode = await
            _tekkenDBContext.tableCode.Where(x => x.tableName == tableName.ToString()).SingleOrDefaultAsync();
            int stateGroupNumber = (stateGroup_code > 0) ? (stateGroup_code - 80000000) * 1000 : 0;

            return tableCode.code + (character_code * 1000) + stateGroupNumber + number;
        }
        #endregion

        #region CreateTranslateName
        public async Task<bool> CreateTranslateNameAllAsync<T>(T translateName) where T : BaseTranslateName
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

        public async Task<bool> CreateTranslateNameAsync<T>(T translateName) where T : BaseTranslateName
        {
            string sql = $"EXECUTE dbo.[TranslateName_GetTranslateNameByCode] " +
                $"@tableName={translateName.GetTableName()}, " +
                $"@base_code={translateName.Base_code}, " +
                $"@name={translateName.Name}, " +
                $"@language_code={translateName.Language_code}";

            var result = await _tekkenDBContext.Database.ExecuteSqlRawAsync(sql);

            return true;
        }

        public abstract List<BaseTranslateName> GetEntity_AllTranslateNamesByCodeAsync(int code);

        protected List<BaseTranslateName> GetAllTranslateNamesByCodeAsync<TEntity>(DbSet<TEntity> databaseSet, int code) where TEntity : BaseTranslateName
        {

            string sql = $"EXECUTE dbo.[TranslateName_GetTranslateNameByCode] " +
                $"@tableName={nameTable}, " +
                $"@base_code={code}";

            List<BaseTranslateName> baseTranslateName = databaseSet.FromSqlRaw(sql).ToList<BaseTranslateName>();

            return baseTranslateName;
        }


        #endregion


        public async Task<BaseTranslateName> UpdateTranslateNameAsync(BaseTranslateName translateName)
        {

            _tekkenDBContext.Entry(translateName).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return translateName;
        }
    }
}