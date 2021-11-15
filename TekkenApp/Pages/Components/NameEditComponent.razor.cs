using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components
{
    public partial class NameEditComponent
    {
        [Parameter]
        public BaseTranslateName baseTranslateName { get; set; }
    }
}
