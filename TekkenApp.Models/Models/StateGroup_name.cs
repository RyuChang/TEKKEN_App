﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

#nullable disable

namespace TekkenApp.Models
{
    //[Index(nameof(Base_code), nameof(language_code), Name = "IX_StateGroup_name", IsUnique = true)]
    public partial class StateGroup_name : BaseNameEntity
    {
        public StateGroup_name()
        {
            SetApp(TableName.StateGroup_name);
        }

        public virtual StateGroup StateGroup_codeNavigation { get; set; }
        public virtual Language language_codeNavigation { get; set; }
    }
}