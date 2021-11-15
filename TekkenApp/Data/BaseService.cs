using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class BaseService
    {
        protected readonly TekkenDbContext _tekkenDBContext;
        protected object dbSet;

        public BaseService(TekkenDbContext tekkenDbContext)
        {
            _tekkenDBContext = tekkenDbContext;
        }



        #region GetRecentBaseModel
        public async Task<BaseEntity> GetRecentBaseModel(TableName tableName, int character_code = 0)
        {
            string sql = $"SELECT ISNULL(MAX(number), 0) + 1 AS Number " +
                       $"FROM [TEKKEN].[dbo].[{tableName}] where 1 = 1";
            BaseEntity result = await _tekkenDBContext.BaseEntity.FromSqlRaw<BaseEntity>(sql).FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region GetCreateNumber
        public async Task<int> GetCreateNumber(TableName tableName)
        {
            BaseEntity result = await GetRecentBaseModel(tableName);
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


        public List<TEntity> GetAllTranslateNamesByCodeAsync<TEntity>(DbSet<TEntity> databaseSet, TEntity translateName) where TEntity : BaseTranslateName
        {
            List<TEntity> baseTranslateName;

            string sql = $"EXECUTE dbo.[TranslateName_GetTranslateNameByCode] " +
                $"@tableName={translateName.GetTableName()}, " +
                $"@base_code={translateName.Base_code}";

            baseTranslateName = databaseSet.FromSqlRaw(sql).ToList();

            return baseTranslateName;
        }
        #endregion


        public async Task<bool> UpdateTranslateNameAsync<TEntity>(DbSet<TEntity> databaseSet, TEntity translateName) where TEntity : BaseTranslateName
        {
            _tekkenDBContext.Entry(translateName).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return true;
        }
    }
}