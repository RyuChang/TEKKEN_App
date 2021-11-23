using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Main
{
    public partial class EditComponent<TEntity, TNameEntity>
                            where TEntity : BaseEntity
                            where TNameEntity : BaseTranslateName, new()
    {
        [Parameter]
        public BaseService<TEntity, TNameEntity> BaseService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        public TEntity BaseEntity { get; set; }


        protected override async Task OnInitializedAsync()
        {
            BaseEntity = await BaseService.GetEntityByIdAsync(Id);
        }

        protected async Task btnSave_Click()
        {
            //await BaseService.CreateEntityAsync(BaseEntity);
            //navigationManager.NavigateTo($"{BaseService.preUrl}/Details_name/{BaseEntity.Id}");
        }

    }
}
