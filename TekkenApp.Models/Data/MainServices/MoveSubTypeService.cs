using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveSubTypeService : BaseNameService<MoveSubType, MoveSubType_name>, IMoveSubTypeService
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public MoveSubTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.moveSubType, tekkenDbContext.moveSubType_name)
        {
            MainTable = TableName.MoveSubType.ToString();
            NameTable = TableName.MoveSubType_name.ToString();
        }



    }
}
