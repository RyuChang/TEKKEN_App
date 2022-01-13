using System.Collections.Generic;

#nullable disable

namespace TekkenApp.Models
{
    //[Index(nameof(code), nameof(number), Name = "IX_moveType_1", IsUnique = true)]
    //[Index(nameof(code), Name = "IX_moveType_code_unique", IsUnique = true)]
    public partial class MoveType : BaseDataEntity<MoveType_name>
    {
        public MoveType()
        {
            SetApp(TableName.MoveType);
            NameSet = new HashSet<MoveType_name>();

        }
    }
}