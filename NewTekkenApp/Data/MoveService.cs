using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class MoveService<TDataEntity, TNameEntity> : BaseService<Move, Move_name>
    {
        [CascadingParameter]
        public int? StateGroupId { get; set; }

        public MoveService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.Move, tekkenDbContext.Move_name)
        {
            mainTable = TableName.Move.ToString();
            nameTable = TableName.Move_name.ToString();
        }



    }
}
