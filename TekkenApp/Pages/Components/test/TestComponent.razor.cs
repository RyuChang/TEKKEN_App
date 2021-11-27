using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Base
{
    public partial class TestComponent<TDataEntity, TNameEntity> : BaseComponent<TDataEntity, TNameEntity>
        where TDataEntity : BaseDataEntity
        where TNameEntity : BaseNameEntity, new()
    {

        public int testNumber { get; set; }
        protected override async Task OnInitializedAsync()
        {
            testNumber = await baseService.GetCreateCode(22);
            //Console.WriteLine(testNumber);
            //return base.OnInitializedAsync();
        }
        public void test()
        {
        }
    }
}
