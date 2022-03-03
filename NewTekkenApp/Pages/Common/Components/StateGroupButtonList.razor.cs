﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Data;

namespace NewTekkenApp.Pages.Common.Components
{
    public partial class StateGroupButtonList
    {
        [Parameter] public EventCallback<string> OnClickCallback { get; set; }
        [Parameter] public Action<int>? OnStateGroupChanged { get; set; }
        [Parameter] public int StateGroupCode { get; set; }

        [Inject] protected IStateGroupService StateGroupService { get; set; } = default!;

        public List<SelectListItem> selectListItems { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            selectListItems = await StateGroupService.GetSelectItems(false);
        }
    }
}