using Microsoft.AspNetCore.Components;
using TekkenApp.Data;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Edit
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        private HitTypeService hitTypeService { get; set; }
    }
}
