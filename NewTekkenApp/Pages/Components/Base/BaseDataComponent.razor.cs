using Microsoft.AspNetCore.Components;
using NewTekkenApp.Utilities;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.Base
{
    public abstract partial class BaseDataComponent<TDataEntity, TNameEntity> : ComponentBase
        where TDataEntity : BaseDataEntity<TNameEntity>
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
