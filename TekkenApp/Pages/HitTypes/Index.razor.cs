using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace TekkenApp.Pages.HitTypes
{
    public partial class Index : BasePageComponent
    {
        public string testValue { get; set; }

        protected override Task OnInitializedAsync()
        {
            testValue = "first";
            return base.OnInitializedAsync();
        }

        private void Test()
        {

            testValue += "teset";
        }
    }
}
