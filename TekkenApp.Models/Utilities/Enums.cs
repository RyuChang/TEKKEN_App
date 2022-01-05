using TekkenApp.Data;

namespace TekkenApp.Models
{
    public static class tableUtil
    {
        static TekkenDbContext tekkenDbContext;

        //public static Dictionary<string, DbSet<T>>
        //    tableSets = new Dictionary<string, DbSet<T>>()
        //    {

        //        { "HitType",  tekkenDbContext.hitType}

        //    };

    }


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
        MoveSubType,
        MoveType,
        MoveType_name,
        Move_Command,
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
