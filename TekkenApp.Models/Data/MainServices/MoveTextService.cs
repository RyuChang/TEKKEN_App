using Microsoft.AspNetCore.Components;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveTextService : BaseService<MoveText, MoveText_name>, IMoveTextService
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
