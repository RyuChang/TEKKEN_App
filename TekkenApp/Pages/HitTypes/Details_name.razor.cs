﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Details_name
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        private HitTypeService hitTypeService { get; set; }



        
    }
}
