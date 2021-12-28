using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class MoveSubTypeService<TDataEntity, TNameEntity> : BaseService<MoveSubType, MoveSubType_name>
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public MoveSubTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.moveSubType, tekkenDbContext.moveSubType_name)
        {
            mainTable = TableName.MoveText.ToString();
            nameTable = TableName.MoveText_name.ToString();
        }



    }
}
