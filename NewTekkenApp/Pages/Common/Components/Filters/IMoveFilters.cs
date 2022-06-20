
namespace NewTekkenApp.Pages.Common.Components.Filters
{
    public interface IMoveFilters
    {
        /// <summary>
        /// The <see cref="ContactFilterColumns"/> being filtered on.
        /// </summary>
        MoveFilterColumns FilterColumn { get; set; }

        /// <summary>
        /// Loading indicator.
        /// </summary>
        bool Loading { get; set; }


        /// <summary>
        /// The text of the filter.
        /// </summary>
        string? FilterText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the sort is ascending or descending.
        /// </summary>
        bool SortAscending { get; set; }

        /// <summary>
        /// The <see cref="MoveFilterColumns"/> being sorted.
        /// </summary>
        MoveFilterColumns SortColumn { get; set; }
    }
}
