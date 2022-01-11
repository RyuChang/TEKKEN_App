using TekkenApp.Data;

namespace TekkenApp.Models
{
    public enum AppType
    {
        States,
        StateGroups,
        HitTypes,
        MoveTexts,
        MoveSubTypes,
        Moves,
        MoveTypes,
        MoveDatas,
        MoveCommands,
    }

    public enum ActionType
    {
        List,
        Create,
        Detail,
        Edit,
        Delete,
        Create_name,
        Detail_name,
        Edit_name,
        Delete_name,
    }

    public enum TableName
    {
        NONE,
        Move,
        Move_name,
        MoveSubType,
        MoveType,
        MoveType_name,
        MoveCommand,
        MoveData,
        MoveData_name,
        Character,
        Character_name,
        Command,
        State,
        State_name,
        StateGroup,
        StateGroup_name,
        MoveText,
        MoveText_name,
        HitType,
        HitType_name
    }


}
