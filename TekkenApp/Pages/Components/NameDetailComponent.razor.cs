using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components
{
    public partial class NameDetailComponent
    {
        [Parameter]
        public BaseTranslateName baseTranslateName { get; set; }
    }
}
