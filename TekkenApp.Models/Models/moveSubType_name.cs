namespace TekkenApp.Models
{
    //[Index(nameof(moveSubType_code), nameof(language_code), Name = "IX_moveSubType_name", IsUnique = true)]
    public partial class MoveSubType_name : BaseNameEntity
    {
        public MoveSubType_name()
        {
            SetApp(TableName.MoveText_name);
        }
    }
}