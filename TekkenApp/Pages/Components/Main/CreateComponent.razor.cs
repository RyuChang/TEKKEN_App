using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Main
{
    public partial class CreateComponent<TEntity, TNameEntity>
                            where TEntity : BaseEntity
                            where TNameEntity : BaseTranslateName, new()
    {
        [Parameter]
        public BaseService<TEntity, TNameEntity> BaseService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        public TEntity BaseEntity { get; set; }


        protected override async Task OnInitializedAsync()
        {
            BaseEntity = Activator.CreateInstance(typeof(TEntity)) as TEntity;
            BaseEntity.Number = await BaseService.GetCreateNumber();
            BaseEntity.Code = await BaseService.GetCreateCode(BaseEntity.Number);
        }

        protected async Task btnSave_Click()
        {
            await BaseService.CreateEntityAsync(BaseEntity);
            navigationManager.NavigateTo($"{BaseService.preUrl}/Details_name/{BaseEntity.Id}");
        }

        

    }
}
