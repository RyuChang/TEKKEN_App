﻿using TekkenApp.Models;

namespace NewTekkenApp.Pages.MoveDatas
{
    public partial class Index : BasePageComponent
    {
        public IList<MoveData> moveDataEntities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitializedAsync();

            //baseEntities = await baseService.GetEntities();

            moveDataEntities = await commonService.GetEntitiesWithMoves();
        }
    }
}