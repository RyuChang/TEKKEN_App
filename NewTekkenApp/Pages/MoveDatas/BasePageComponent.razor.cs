﻿using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveDatas
{
    public partial class BasePageComponent : BaseComponent<MoveData, MoveData_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected IMoveDataService CommonService { get; set; } = default!;

        [Inject]
        protected IMoveTypeService moveTypeService { get; set; } = default!;


        [Inject]
        protected IMoveSubTypeService moveSubTypeService { get; set; } = default!;

        [Inject]
        protected IHitTypeService hitTypeService { get; set; } = default!;

        public BasePageComponent()
        {
            SetAppType(AppType.MoveDatas);
        }
    }
}
