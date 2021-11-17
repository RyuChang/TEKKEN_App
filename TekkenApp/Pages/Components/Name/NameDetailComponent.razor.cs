using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.Components.Name
{
    public partial class NameDetailComponent<TEntity, TNameEntity>
                                            where TEntity : BaseEntity
                                            where TNameEntity : BaseTranslateName
    {
        [Parameter]
        public BaseService<TEntity, TNameEntity> baseService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public TNameEntity nameEntity { get; set; }


        NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            nameEntity = await baseService.GetNameEntityByIdAsync(Id);
        }

        protected void btnList_Click()
        {
            //navigationManager.NavigateTo($"{baseTranslateName.preUrl}");
        }
    }
}
