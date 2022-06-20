using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace NewTekkenApp.Pages.Common.Components.Filters
{
    public class MoveQueryAdapter
    {
        /// <summary>
        /// Holds state of the grid.
        /// </summary>
        private readonly IMoveFilters _controls;


        /// <summary>
        /// Expressions for sorting.
        /// </summary>
        private readonly Dictionary<MoveFilterColumns, Expression<Func<Move, string>>> _expressions
            = new()
            {
                { MoveFilterColumns.Title, c => c != null && c.Description != null ? c.Description: string.Empty },
            };


        /// <summary>
        /// Queryables for filtering.
        /// </summary>
        private readonly Dictionary<MoveFilterColumns, Func<IQueryable<Move>, IQueryable<Move>>> _filterQueries =
            new Dictionary<MoveFilterColumns, Func<IQueryable<Move>, IQueryable<Move>>>();

        public MoveQueryAdapter(IMoveFilters controls)
        {
            _controls = controls;
            _filterQueries = new()
            {
                { MoveFilterColumns.Title, cs => cs.Where(c => c != null && c.Description != null && _controls.FilterText != null ? c.Description.Contains(_controls.FilterText) : false ) },

            };
        }

        /// <summary>
        /// Uses the query to return a count and a page.
        /// </summary>
        /// <param name="query">The <see cref="IQueryable{Move}"/> to work from.</param>
        /// <returns>The resulting <see cref="ICollection{Move}"/>.</returns>
        public async Task<ICollection<Move>> FetchAsync(IQueryable<Move> query)
        {
            query = FilterAndQuery(query);
            //await CountAsync(query);
            var collection = await FetchPageQuery(query).ToListAsync();
            //_controls.PageHelper.PageItems = collection.Count;
            return collection;
        }

        /// <summary>
        /// Build the query to bring back a single page.
        /// </summary>
        /// <param name="query">The <see cref="IQueryable{Move}"/> to modify.</param>
        /// <returns>The new <see cref="IQueryable{Move}"/> for a page.</returns>
        public IQueryable<Move> FetchPageQuery(IQueryable<Move> query)
        {
            return query
                //.Skip(_controls.PageHelper.Skip)
                //.Take(_controls.PageHelper.PageSize)
                .AsNoTracking();
        }



        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <param name="root">The <see cref="IQueryable{Move}"/> to start with.</param>
        /// <returns>
        /// The resulting <see cref="IQueryable{Move}"/> with sorts and
        /// filters applied.
        /// </returns>
        private IQueryable<Move> FilterAndQuery(IQueryable<Move> root)
        {
            var sb = new System.Text.StringBuilder();

            // apply a filter?
            if (!string.IsNullOrWhiteSpace(_controls.FilterText))
            {
                var filter = _filterQueries[_controls.FilterColumn];
                sb.Append($"Filter: '{_controls.FilterColumn}' ");
                root = filter(root);
            }

            //apply the expression
            var expression = _expressions[_controls.SortColumn];
            sb.Append($"Sort: '{_controls.SortColumn}' ");

            // fix up name
            /*if (_controls.SortColumn == ContactFilterColumns.Name && _controls.ShowFirstNameFirst)
            {
                sb.Append($"(first name first) ");
                expression = c => c.FirstName != null ? c.FirstName : string.Empty;
            }*/

            var sortDir = _controls.SortAscending ? "ASC" : "DESC";
            sb.Append(sortDir);

            Debug.WriteLine(sb.ToString());
            // return the unfiltered query for total count, and the filtered for fetching
            return _controls.SortAscending ? root.OrderBy(expression) : root.OrderByDescending(expression);
        }
    }
}