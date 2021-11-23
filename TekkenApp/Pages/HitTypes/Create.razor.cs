using Microsoft.AspNetCore.Components;
using TekkenApp.Data;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Create
    {
        [Inject]
        private HitTypeService hitTypeService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }
    }
}

