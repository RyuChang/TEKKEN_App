// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

#nullable disable

namespace TekkenApp.Models
{
    [Index(nameof(Code), Name = "IX_StateGroup", IsUnique = true)]
    [Index(nameof(Number), Name = "IX_StateGroup_1", IsUnique = true)]

    public partial class StateGroup : BaseDataEntity<StateGroup_name>
    {
        public StateGroup()
        {
            SetApp(TableName.StateGroup);
            //State = new HashSet<State>();
            NameSet = new HashSet<StateGroup_name>();
        }

        //public virtual ICollection<State> State { get; set; }
        //public virtual ICollection<StateGroup_name> StateGroup_name { get; set; }
    }
}