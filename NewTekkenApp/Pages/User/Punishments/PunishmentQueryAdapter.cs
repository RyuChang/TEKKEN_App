using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NewTekkenApp.Pages.Common.Components.Filters;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.User.Punishments
{

    public class CaseInsensitiveComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, true);
        }
    }

    public class PunishmentQueryAdapter
    {
        /// <summary>
        /// Holds state of the grid.
        /// </summary>
        private readonly IMoveFilters _controls;
        private TekkenDbContext dbContext { get; set; }

        /// <summary>
        /// Expressions for sorting.
        /// </summary>
        private readonly Dictionary<MoveFilterColumns, Expression<Func<Move, string>>> _stringOrderExpressions
            = new()
            {
                { MoveFilterColumns.Title, c => c.NameSet.Where(n => n.Language_code.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)).FirstOrDefault().Name },
                { MoveFilterColumns.Number, c => string.Empty},
                { MoveFilterColumns.Hits, c => string.Empty },
            };

        private readonly Dictionary<MoveFilterColumns, Expression<Func<Move, int>>> _NumberOrderExpressions
            = new()
            {
                { MoveFilterColumns.Title, c => 0 },
                { MoveFilterColumns.Number, c => c != null && c.Number != 0 ? c.Number : 0},
                { MoveFilterColumns.Hits, c => c != null && c.MoveData.HitCount!= null ? c.MoveData.HitCount : 0},
            };


        /// <summary>
        /// Queryables for filtering.
        /// </summary>
        private readonly Dictionary<MoveFilterColumns, Func<IQueryable<Move>, IQueryable<Move>>> _textFilterQueries =
            new Dictionary<MoveFilterColumns, Func<IQueryable<Move>, IQueryable<Move>>>();

        private readonly Dictionary<MoveFilterColumns, Func<IQueryable<Move>, IQueryable<Move>>> _commandFilterQueries =
            new Dictionary<MoveFilterColumns, Func<IQueryable<Move>, IQueryable<Move>>>();

        public PunishmentQueryAdapter(TekkenDbContext tekkenDbContext, IMoveFilters controls)
        {
            dbContext = tekkenDbContext;
            _controls = controls;
            _textFilterQueries = new()
            {
                { MoveFilterColumns.Title, cs => cs.Where(c => c != null && c.Description != null && _controls.FilterText != null ? c.NameSet.Where(n => n.Language_code.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)).FirstOrDefault().Name.Contains(_controls.FilterText) : false)
                 }
            };
            _commandFilterQueries = new()
            {
                { MoveFilterColumns.Command, cs => cs.Where(c => c != null && c.MoveCommand.Command!= null && _controls.FilterCommand!= null ? c.MoveCommand.Command.Contains(_controls.FilterCommand) : false )
                 }
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
            await CountAsync(query);
            var collection = await FetchPageQuery(query)
                .Include(d => d.NameSet.Where(n => n.Language_code.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)))
                .Include(m => m.MoveCommand).ThenInclude(c => c.NameSet.Where(n => n.Language_code.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)))
                .Include(m => m.MoveData).ThenInclude(c => c.NameSet.Where(n => n.Language_code.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)))
                .ToListAsync();

            _controls.PageHelper.PageItems = collection.Count;
            return collection;
        }

        /// <summary>
        /// Get total filtered items count.
        /// </summary>
        /// <param name="query">The <see cref="IQueryable{Contact}"/> to use.</param>
        /// <returns>Asynchronous <see cref="Task"/>.</returns>
        public async Task CountAsync(IQueryable<Move> query)
        {
            _controls.PageHelper.TotalItemCount = await query.CountAsync();
        }


        /// <summary>
        /// Build the query to bring back a single page.
        /// </summary>
        /// <param name="query">The <see cref="IQueryable{Move}"/> to modify.</param>
        /// <returns>The new <see cref="IQueryable{Move}"/> for a page.</returns>
        public IQueryable<Move> FetchPageQuery(IQueryable<Move> query)
        {
            return query
                .Skip(_controls.PageHelper.Skip)
                .Take(_controls.PageHelper.PageSize)
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

            //root = root.Where(n => n.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)).AsQueryable<Move>();

            var sb = new System.Text.StringBuilder();

            // apply a filter?
            if (!string.IsNullOrWhiteSpace(_controls.FilterText))
            {
                _controls.FilterColumn = MoveFilterColumns.Title;
                var textFilter = _textFilterQueries[_controls.FilterColumn];
                sb.Append($"Filter: '{_controls.FilterColumn}' ");
                root = textFilter(root);
            }

            if (!string.IsNullOrWhiteSpace(_controls.FilterCommand))
            {
                _controls.FilterColumn = MoveFilterColumns.Command;
                var commandFilter = _commandFilterQueries[_controls.FilterColumn];
                sb.Append($"Filter: '{_controls.FilterColumn}' ");
                root = commandFilter(root);
            }
            //apply the expression
            var stringExpression = _stringOrderExpressions[_controls.SortColumn];
            var numberExpression = _NumberOrderExpressions[_controls.SortColumn];


            sb.Append($"Sort: '{_controls.SortColumn}' ");

            var sortDir = _controls.SortAscending ? "ASC" : "DESC";
            sb.Append(sortDir);

            Debug.WriteLine(sb.ToString());
            if (_controls.SortColumn == MoveFilterColumns.Title)
            {
                return _controls.SortAscending ? root.OrderBy(stringExpression) : root.OrderByDescending(stringExpression);
            }
            return _controls.SortAscending ? root.OrderBy(numberExpression) : root.OrderByDescending(numberExpression);
        }
    }
}