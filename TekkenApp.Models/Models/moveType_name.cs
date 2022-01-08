
namespace TekkenApp.Models
{
    //   [Index(nameof(language_code), nameof(moveType_code), Name = "IX_moveType_name", IsUnique = true)]
    public partial class MoveType_name : BaseNameEntity
    {
        public MoveType_name()
        {
            SetApp(TableName.MoveType_name);
        }
    }
}