﻿using System;
using System.Linq;
using System.Reflection;
using TekkenApp.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

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


    public enum APP
    {
        StateGroup,
        HitType,
    }

    public enum TableName
    {
        NONE,
        move,
        moveSubType,
        moveType,
        move_Command,
        move_Data,
        character,
        command,
        State,
        StateGroup,
        StateGroup_name,
        MoveText,
        HitType,
        HitType_name
    }


}