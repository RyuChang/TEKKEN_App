using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using TekkenApp.Data;
using TekkenApp.Models;
using TekkenApp.Pages.Components.Base;

namespace TekkenApp.Pages.Components.SharedComponents
{
    public partial class StateGroupSelectBox
    {
        [Inject]
        protected StateGroupService<StateGroup, StateGroup_name> stateGroupService { get; set; }


        public List<StateGroup> groupList { get; set; }
        public StateGroup entity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //SetApp();
            //groupList = await stateGroupService.GetStateGroups();
            entity = await stateGroupService.GetDataEntityByIdAsync(80000004);

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