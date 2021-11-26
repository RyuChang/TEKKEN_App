﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Name
{
    public partial class CreateNameComponent<TDataEntity, TNameEntity>
                                            where TDataEntity : BaseDataEntity
                                            where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public BaseService<TDataEntity, TNameEntity> BaseService { get; set; }

        [Parameter]
        public int Code { get; set; }

        [Parameter]
        public string Language { get; set; }

        public TNameEntity BaseNameEntity = new();

        [Inject]
        NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            BaseNameEntity.Base_code = Code;
            BaseNameEntity.Language_code = Language;
        }

        protected void btnCancel_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}");
        }

        protected void btnList_Click()
        {
            navigationManager.NavigateTo($"{BaseService.preUrl}");
        }

        protected async Task btnDelete_Click()
        {
            //await hitTypeService.UpdateHitTypeNameAsync(hitType_name);
            //navigationManager.NavigateTo($"/HitTypes/Detail_name/{Id}");
        }
        protected async Task btnSave_Click()
        {
            await BaseService.CreateTranslateNameAsync(BaseNameEntity);
            navigationManager.NavigateTo($"{BaseService.preUrl}/Detail_name/{BaseNameEntity.Base_code}");
        }
    }
}
