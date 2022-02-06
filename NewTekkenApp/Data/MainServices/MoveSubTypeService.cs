using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class MoveSubTypeService : BaseService<MoveSubType, MoveSubType_name>, IMoveSubTypeService
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public MoveSubTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.moveSubType, tekkenDbContext.moveSubType_name)
        {
            MainTable = TableName.MoveText.ToString();
            NameTable = TableName.MoveText_name.ToString();
        }



    }
}
