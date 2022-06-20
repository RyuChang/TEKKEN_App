namespace NewTekkenApp.Pages.Common.Components.Filters
{
    public class ListControls : IMoveFilters
    {

        public ListControls()
        {

        }

        /// <summary>
        /// Column filtered text is against.
        /// </summary>
        public MoveFilterColumns FilterColumn { get; set; } = MoveFilterColumns.Title;

        /// <summary>
        /// Avoid multiple concurrent requests.
        /// </summary>
        public bool Loading { get; set; }

        /// <summary>
        /// Text to filter on.
        /// </summary>
        public string? FilterText { get; set; }

        /// <summary>
        /// True when sorting ascending, otherwise sort descending.
        /// </summary>
        public bool SortAscending { get; set; } = true;

        public MoveFilterColumns SortColumn { get; set; }
            = MoveFilterColumns.Title;
    }
}

