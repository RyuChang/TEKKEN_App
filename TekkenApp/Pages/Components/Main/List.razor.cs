using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Main
{
    public partial class List<TEntity, TNameEntity>
                where TEntity : BaseEntity
        where TNameEntity : BaseTranslateName
    {
        [Parameter]
        public BaseService<TEntity, TNameEntity> baseService { get; set; }

        [Parameter]
        public IList<TEntity> baseEntities { get; set; }



        protected override async void OnInitialized()
        {
            baseEntities = await baseService.GetEntities();
        }



        //[Inject]
        //NavigationManager navigationManager { get; set; }

    }
}
