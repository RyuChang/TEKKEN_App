using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace NewTekkenApp.Pages.HitTypes
{
    public partial class Create_name : BasePageComponent
    {
        [Parameter]
        public int Code { get; set; }

        [Parameter]
        public string Language { get; set; }

        protected override Task OnInitializedAsync()
        {
            
            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("Language", out var _language))
            {
                Language = _language;
            }
            if (queryStrings.TryGetValue("Code", out var _code))
            {
                Code = int.Parse(_code);
            }

            return base.OnInitializedAsync();
        }
    }
}
