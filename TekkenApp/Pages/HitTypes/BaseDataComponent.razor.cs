using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;
using TekkenApp.Utilities;

namespace TekkenApp.Pages.HitTypes
{
    public partial class BaseDataComponent
    {
        protected AppType appType = AppType.HitTypes;

        [Parameter]
        public int Id { get; set; }

        [Inject]
        protected HitTypeService commonService { get; set; }

        [Inject]
        protected NavigationUtil navigationUtil { get; set; }

        public string GetAppTitle()
        {
            return appType.ToString();
        }
    }
}
