using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class StateService : BaseService<State, State_name>, IStateService
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public StateService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.State, tekkenDbContext.State_name)
        {
            MainTable = TableName.State.ToString();
            NameTable = TableName.State.ToString();
        }

    }
}
