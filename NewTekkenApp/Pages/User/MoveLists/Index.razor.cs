using System.Globalization;
using System.Runtime.CompilerServices;
using System.Linq;
using Microsoft.AspNetCore.Components;
using NewTekkenApp.Pages.Common.Components.Filters;
using NewTekkenApp.Shared;
using TekkenApp.Models;
using TekkenApp.Data;
using static NuGet.Packaging.PackagingConstants;

namespace NewTekkenApp.Pages.User.MoveLists
{
    public partial class Index : BasePageComponent
    {
        [Inject] IMoveFilters moveFilters { get; set; }
        [Inject] MoveQueryAdapter QueryAdapter { get; set; }
        public IEnumerable<Move> moveLists { get; set; } = default!;

        /// <summary>
        /// A wrapper for grid-related activity (like delete).
        /// </summary>
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
                //moveLists = await CommonService?.GetMoveListWithCommandsByCharacterCodeAsync(CharacterCode);
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

            /*

            if (Wrapper is not null)
            {
                Wrapper.DeleteRequestId = 0;
            }

            Contacts = new List<Contact>();

            */

            //using var context = CommonService.GetDbSet();
            var moveDbSet = CommonService?.GetDbSet();
            var query = moveDbSet?.AsQueryable().Where(m => m.Character_code == CharacterCode)
                ;

            //var query = CommonService?.GetMoveListWithCommandsByCharacterCodeAsync(CharacterCode).Result.AsQueryable();

            if (query is not null)
            {

                // run the query to load the current page
                //Contacts = await QueryAdapter.FetchAsync(query);
                moveLists = await QueryAdapter.FetchAsync(query);

            }
            /*
            */
            // now we're done
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
