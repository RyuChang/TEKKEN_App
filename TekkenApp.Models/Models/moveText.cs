using System.Collections.Generic;

namespace TekkenApp.Models
{
    //    [Index(nameof(character_code), nameof(number), Name = "IX_moveText_character_code_number", IsUnique = true)]
    //   [Index(nameof(code), Name = "IX_moveText_code", IsUnique = true)]
    public partial class MoveText : BaseDataEntity<MoveText_name>
    {
        public MoveText()
        {
            SetApp(TableName.MoveText);
            NameSet = new HashSet<MoveText_name>();
        }

        public new int Character_code { get; set; }

        //public virtual Character character_codeNavigation { get; set; }
        //public virtual ICollection<MoveText_name> moveText_name { get; set; }
    }
}