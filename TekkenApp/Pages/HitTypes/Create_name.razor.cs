using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Create_name
    {
        [Parameter]
        public int Code { get; set; }

        [Parameter]
        public string Language { get; set; }

        [Inject]
        private HitTypeService hitTypeService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        protected override Task OnInitializedAsync()
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);

            if (queryStrings.TryGetValue("Language", out var _language))
            {
                Language = _language;
            }
            return base.OnInitializedAsync();
        }
    }
}


/* private void Navigate()
    {
        var queryParams = new Dictionary<string, string>
        {
            ["name"] = "John"
        };
        NavManager.NavigateTo(QueryHelpers.AddQueryString("query-string-display", queryParams));
    }
*/