using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
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
