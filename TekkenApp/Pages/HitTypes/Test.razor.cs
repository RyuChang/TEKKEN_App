using Microsoft.AspNetCore.Components;
using TekkenApp.Data;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Test
    {
        [Inject]
        private HitTypeService hitTypeService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }
    }
}

