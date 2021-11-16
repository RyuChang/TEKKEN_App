﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Name
{
    public partial class NameEditComponent<TEntity, TNameEntity> where TEntity : BaseEntity
        where TNameEntity : BaseTranslateName
    {
        [Parameter]
        public BaseTranslateName baseTranslateName { get; set; }

        [Parameter]
        public BaseService<TEntity,TNameEntity> baseService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        protected async Task btnEdit_Click()
        {
            await baseService.UpdateTranslateNameAsync(baseTranslateName);
            navigationManager.NavigateTo($"{baseTranslateName.preUrl}/Details_name/{baseTranslateName.Id}");
        }

        protected void btnList_Click()
        {
            navigationManager.NavigateTo($"{baseTranslateName.preUrl}");
        }

    }
}
