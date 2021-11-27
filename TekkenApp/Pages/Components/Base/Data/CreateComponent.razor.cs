﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base.Data
{
    public partial class CreateComponent<TDataEntity, TNameEntity> :
        BaseComponent<TDataEntity, TNameEntity>
                            where TDataEntity : BaseDataEntity
                            where TNameEntity : BaseNameEntity, new()
    {

        protected override async Task OnInitializedAsync()
        {
            BaseDataEntity = Activator.CreateInstance(typeof(TDataEntity)) as TDataEntity;
            BaseDataEntity.Number = await baseService.GetCreateNumber();
            BaseDataEntity.Code = await baseService.GetCreateCode(BaseDataEntity.Number);
        }

        protected async Task SaveCreate()
        {
            await baseService.CreateEntityAsync(BaseDataEntity);
            MoveToDetail(BaseDataEntity.Id);
        }
    }
}
