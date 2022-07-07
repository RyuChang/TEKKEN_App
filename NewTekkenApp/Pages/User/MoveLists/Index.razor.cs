using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Filters;
using NewTekkenApp.Shared;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.User.MoveLists
{
    public partial class Index : BasePageComponent
    {
        [Inject] IMoveFilters moveFilters { get; set; }
        [Inject] MoveListQueryAdapter QueryAdapter { get; set; }
        public IEnumerable<Move> moveLists { get; set; } = default!;

        private GridWrapper Wrapper { get; set; } = new GridWrapper();

        protected async void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            if (Loading)
            {
                return;
            }
            try
            {
                Loading = true;
                await ReloadAsync();
            }
            finally
            {
                Loading = false;
            }
            StateHasChanged();

        }

        private async Task ReloadAsync()
        {
            if (moveFilters.Loading)
            {
                return;
            }

            moveFilters.Loading = true;

            var moveDbSet = MoveService?.GetDbSet();
            var query = moveDbSet?.AsQueryable().Where(m => m.Character_code == CharacterCode);

            if (query is not null)
            {
                moveLists = await QueryAdapter.FetchAsync(query);
            }
            moveFilters.Loading = false;
        }

        /// <summary>
        /// Used to toggle the grid sort. Will either switch to "ascending" on a new
        /// column, or toggle beteween "ascending" and "descending" on a column with the
        /// sort already set.
        /// </summary>
        /// <param name="col">The <see cref="ContactFilterColumns"/> to toggle.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        private Task ToggleAsync(MoveFilterColumns col)
        {
            if (moveFilters.SortColumn == col)
            {
                moveFilters.SortAscending = !moveFilters.SortAscending;
            }
            else
            {
                moveFilters.SortColumn = col;
            }
            return ReloadAsync();
        }
    }
}
