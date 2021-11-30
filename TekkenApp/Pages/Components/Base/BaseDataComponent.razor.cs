using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;
using TekkenApp.Utilities;

namespace TekkenApp.Pages.Components.Base
{
    public abstract partial class BaseDataComponent<TDataEntity, TNameEntity> : ComponentBase
        where TDataEntity : BaseDataEntity
        where TNameEntity : BaseNameEntity, new()
    {
        protected AppType appType;

        [Parameter]
        public int Id { get; set; }


        [Inject]
        protected NavigationUtil navigationUtil { get; set; }

        public string GetAppTitle()
        {
            return appType.ToString();
        }

        public void SetAppType(AppType appType)
        {
            this.appType = appType;
        }
    }
}
