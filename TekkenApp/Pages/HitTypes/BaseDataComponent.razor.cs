using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Utilities;

namespace TekkenApp.Pages.HitTypes
{
    public partial class BaseDataComponent
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        protected HitTypeService hitTypeService { get; set; }

        [Inject]
        protected NavigationUtil navigationUtil { get; set; }
    }
}
