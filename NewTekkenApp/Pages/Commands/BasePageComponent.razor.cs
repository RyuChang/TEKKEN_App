using Microsoft.AspNetCore.Components;
using NewTekkenApp.Data;
using NewTekkenApp.Pages.Components.Base;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Commands
{
    public partial class BasePageComponent : BaseDataComponent<Command, Command_name>
    {

        [Inject]
        protected CommandService<Command, Command_name> CommonService { get; set; } = default!;

        public BasePageComponent()
        {
            SetAppType(AppType.Commands);
        }
    }
}
