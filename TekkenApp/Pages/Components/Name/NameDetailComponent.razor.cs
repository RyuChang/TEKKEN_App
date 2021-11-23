using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Name
{
    public partial class NameDetailComponent<TEntity, TNameEntity>
                                            where TEntity : BaseEntity
                                            where TNameEntity : BaseTranslateName, new()
    {
        [Parameter]
        public BaseService<TEntity, TNameEntity> BaseService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public TNameEntity nameEntity { get; set; }
        
        [Inject]
        NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            nameEntity = await BaseService.GetNameEntityByIdAsync(Id);
        }

        protected void btnList_Click()
        {
            //navigationManager.NavigateTo($"{baseTranslateName.preUrl}");
        }
    }
}
