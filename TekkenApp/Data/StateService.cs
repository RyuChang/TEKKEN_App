using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
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
