using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Base;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.Commands
{
    public partial class BasePageComponent : BaseDataComponent<Command, Command_name>
    {

        [Inject]
        protected ICommandService CommonService { get; set; } = default!;

        public BasePageComponent()
        {
            SetAppType(AppType.Commands);
        }
    }
}
