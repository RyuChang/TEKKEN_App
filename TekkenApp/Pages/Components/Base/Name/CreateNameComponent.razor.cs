using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Name
{
    public partial class CreateNameComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public int Code { get; set; }

        [Parameter]
        public string Language { get; set; }

        protected override async Task OnInitializedAsync()
        {
            baseName = new();
            baseName.Base_code = Code;
            baseName.Language_code = Language;
        }

        protected async Task SaveCreate()
        {
            await baseService.CreateNameEntityAsync(baseName);
            MoveToDetailName(baseName.Id);
        }
    }
}
