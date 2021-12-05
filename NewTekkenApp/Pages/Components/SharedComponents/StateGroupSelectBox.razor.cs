using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewTekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.SharedComponents
{
    public partial class StateGroupSelectBox
    {
        [Inject]
        protected StateGroupService<StateGroup, StateGroup_name> stateGroupService { get; set; }

        public List<SelectListItem> selectListItems { get; set; }

        [Parameter]
        public EventCallback<string> OnClickCallback { get; set; }

        [CascadingParameter]
        public int StateGroupId { get; set; }

        public string stateGroup { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //SetApp();
            //groupList = await stateGroupService.GetStateGroups();
            selectListItems = await stateGroupService.GetSelectItems();

        }

        private void OnStateGroupChanged(ChangeEventArgs e)
        {
            stateGroup = e.Value.ToString();
            StateGroupId = int.Parse(e.Value.ToString());
            //stateGroup = SelectedString;
            OnClickCallback.InvokeAsync();
        }


    }
}


//< EditForm Model = "item" >


//    < InputSelect @bind - Value = "item.CountryId" >


//        < option value = "" ></ option >
//         @foreach(var c in countries)
//      {
//         < option value = "@c.Id" > @c.Name </ option >
//      }
//   </ InputSelect >