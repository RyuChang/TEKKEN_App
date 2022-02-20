using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class LanguageService : BaseNameService<Language, Language_name>
    {
        public LanguageService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.State, tekkenDbContext.State_name)
        {
            MainTable = TableName.State.ToString();
            NameTable = TableName.State.ToString();
        }

    }
}
