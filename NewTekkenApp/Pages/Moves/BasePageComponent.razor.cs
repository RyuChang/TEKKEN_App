﻿using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Moves
{
    public partial class BasePageComponent : BaseDataComponent<Move, Move_name>
    {
        public int? CharacterId { get; set; }

        [Inject]
        protected MoveService<Move, Move_name> commonService { get; set; }

        public BasePageComponent()
        {
            SetAppType(AppType.Moves);
        }
    }
}