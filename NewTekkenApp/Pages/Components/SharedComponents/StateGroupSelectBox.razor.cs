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

        protected override async Task OnInitializedAsync()
        {
            selectListItems = await stateGroupService.GetSelectItems();
        }

        [Parameter]
        public Action<String>? OnStateGroupChanged { get; set; }

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