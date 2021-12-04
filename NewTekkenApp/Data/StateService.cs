using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class StateService<TDataEntity, TNameEntity> : BaseService<State, State_name>
    {

        public StateService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.State, tekkenDbContext.State_name)
        {
            {

            }
        }
    }
}
