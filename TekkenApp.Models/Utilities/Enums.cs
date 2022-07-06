﻿namespace TekkenApp.Models
{
    public enum UserType
    {
        User,
        Admin,
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
        MoveVideo,
        MoveCommands,
        Commands,
        Punishments,
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
        Edit_number,
        Delete_name,
    }

    public enum TableName
    {
        NONE,
        Move,
        Move_name,
        MoveSubType,
        MoveSubType_name,
        MoveType,
        MoveType_name,
        MoveCommand,
        MoveCommand_name,
        MoveData,
        MoveData_name,
        MoveVideo,
        MoveVideo_name,
        Character,
        Character_name,
        Command,
        Command_name,
        State,
        State_name,
        StateGroup,
        StateGroup_name,
        MoveText,
        MoveText_name,
        HitType,
        HitType_name,
        Language,
    }
}
