using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class LanguageService : BaseDataService<Language>, ILanguageService
    {
        public LanguageService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.Language)
        {
            MainTable = TableName.State.ToString();
        }

    }
}
