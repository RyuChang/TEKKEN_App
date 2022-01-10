using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class MoveTextService<TDataEntity, TNameEntity> : BaseService<MoveText, MoveText_name>
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public MoveTextService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.moveText, tekkenDbContext.moveText_name)
        {
            MainTable = TableName.MoveText.ToString();
            NameTable = TableName.MoveText_name.ToString();
        }



    }
}
