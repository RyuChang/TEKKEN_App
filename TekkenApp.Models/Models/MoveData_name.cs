﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TekkenApp.Models
{
    //[Index(nameof(Move_Data_Code), nameof(language_code), Name = "IX_Move_Data_Name", IsUnique = true)]
    public partial class MoveData_name : BaseNameEntity
    {
        [NotMapped]
        public string Name { get; set; }

        //public string moveType_name { get; set; }
        //public string moveSubType_name { get; set; }
        public string StartType_name { get; set; }
        public string Guardtype_name { get; set; }
        public string HitType_name { get; set; }
        public string CounterType_name { get; set; }
        //public string note_name { get; set; }


        //public virtual MoveData Move_Data_CodeNavigation { get; set; }
    }
}