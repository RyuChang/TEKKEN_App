using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Name
{
    public partial class NameDetailComponent
    {
        [Parameter]
        public BaseTranslateName baseTranslateName { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        protected void btnList_Click()
        {
            navigationManager.NavigateTo($"{baseTranslateName.preUrl}");
        }
    }
}
