using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class StateService<TDataEntity, TNameEntity> : BaseService<State, State_name>
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public StateService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.State, tekkenDbContext.State_name)
        {
            mainTable = TableName.State.ToString();
            nameTable = TableName.State.ToString();
        }

    }
}
