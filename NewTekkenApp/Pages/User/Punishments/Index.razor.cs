using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using NewTekkenApp.Pages.Common.Components.Filters;
using NewTekkenApp.Pages.User.MoveLists;
using NewTekkenApp.Shared;
using TekkenApp.Models;
using static NuGet.Packaging.PackagingConstants;


namespace NewTekkenApp.Pages.User.Punishments
{
    public partial class Index : BasePageComponent
    {
        [Inject] IMoveFilters moveFilters { get; set; }

        [Inject] PunishmentQueryAdapter QueryAdapter { get; set; }
        [Inject] NavigationManager Nav { get; set; }


        /// <summary>
        /// The current page.
        /// </summary>
        [Parameter]
        public int Page
        {
            get => moveFilters.PageHelper.Page;
            set
            {
                moveFilters.PageHelper.Page = value;
            }
        }
        public IEnumerable<Move> moveLists { get; set; } = default!;

        private GridWrapper Wrapper { get; set; } = new GridWrapper();

        /// <summary>
        /// Helper method to set disabled on class for paging.
        /// </summary>
        /// <param name="condition"><c>true</c> when the element is active (and therefore should not be disabled).</param>
        /// <returns>The string literal "disabled" or an empty string.</returns>
        private string IsDisabled(bool condition) =>
            !moveFilters.Loading && condition ? "" : "disabled";

        /// <summary>
        /// Keeps track of the last page loaded.
        /// </summary>
        private int _lastPage = -1;

        /// <summary>
        /// Main logic when getting started.
        /// </summary>
        /// <param name="firstRender"><c>true</c> for first-time render.</param>
        protected override void OnAfterRender(bool firstRender)
        {
            // Ensure we're on the same, er, right page.
            if (_lastPage < 1)
            {
                Nav.NavigateTo("/User/Punishments/1");
                return;
            }

            // Normalize the page values.
            if (moveFilters.PageHelper.PageCount > 0)
            {
                if (Page < 1)
                {
                    Nav.NavigateTo("/User/Punishments/1");
                    return;
                }
                if (Page > moveFilters.PageHelper.PageCount)
                {
                    Nav.NavigateTo($"/User/Punishments/{moveFilters.PageHelper.PageCount}");
                    return;
                }
            }
            base.OnAfterRender(firstRender);
        }

        /// <summary>
        /// Triggered for any paging update.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        protected override async Task OnParametersSetAsync()
        {
            // Make sure the page really chagned.
            if (Page != _lastPage)
            {
                _lastPage = Page;
                await ReloadAsync();
            }
            await base.OnParametersSetAsync();
        }


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
            if (moveFilters.Loading || Page < 1)
            {
                return;
            }

            moveFilters.Loading = true;

            var moveDbSet = MoveService?.GetDbSet();
            var query = moveDbSet?.AsQueryable().Where(m => m.Character_code == CharacterCode)
                .Include(m => m.MoveVideo)
                .Include(m => m.MoveCommand)
                .ThenInclude(c => c.NameSet.Where(n => n.Language_code.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)))
                .Include(m => m.MoveData)
                .ThenInclude(c => c.NameSet);
            //moveLists = await CommonService?.GetMoveListWithCommandsAndVideoByCharacterCodeAsync(CharacterCode);

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
