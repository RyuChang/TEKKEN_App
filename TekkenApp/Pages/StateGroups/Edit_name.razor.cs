using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.StateGroups
{
    public partial class Edit_name
    {
        [Parameter]
        public string Id { get; set; }

        public StateGroup_name stateGroup_name = new StateGroup_name();

        [Inject]
        private StateGroupService stateGroupService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(Id, out int id))
            {

            }
            stateGroup_name = await stateGroupService.GetStateGroupNameByIdAsync(id);
        }


        protected async Task btnEdit_Click()
        {
            //await stateGroupService.UpdateStateGroupNameAsync(stateGroup_name);
            navigationManager.NavigateTo($"/StateGroup/Details_name/{Id}");
        }

        protected void btnCancel_click()
        {
            navigationManager.NavigateTo("/Videos");
        }
    }
}
