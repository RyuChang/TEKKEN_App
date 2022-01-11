﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TekkenApp.Models
{
    ///[Index(nameof(Move_Code), Name = "IX_move_data", IsUnique = true)]
    public partial class MoveData : BaseDataEntity<MoveData_name>
    {
        public MoveData()
        {
            SetApp(TableName.MoveData);
            NameSet = new HashSet<MoveData_name>();
        }

        public int Base_Code { get; set; }
        public int? MoveType_code { get; set; }
        public int? MoveSubType_code { get; set; }
        public int HitCount { get; set; }
        [Required]
        public string HitLevel { get; set; }
        public int Damage { get; set; }
        public decimal StartFrame { get; set; }
        public string StartFrame_Display { get; set; }
        public int? StartType_code { get; set; }
        public int GuardFrame { get; set; }
        public string GuardFrame_Display { get; set; }
        public int GuardType_code { get; set; }
        public int HitFrame { get; set; }
        public string HitFrame_Display { get; set; }
        public int HitType_code { get; set; }
        public int CounterFrame { get; set; }
        public string CounterFrame_Display { get; set; }
        public int CounterType_code { get; set; }
        public string BreakThrow { get; set; }
        public string AfterBreak { get; set; }
        public bool Homing { get; set; }
        public bool PowerCrush { get; set; }
        public bool TechnicallyCrouching { get; set; }
        public bool TechnicallyJumping { get; set; }
        public bool TailSpin { get; set; }
        public bool WallSplat { get; set; }
        public string Note { get; set; }
        //[Column(TypeName = "decimal(4, 2)")]
        public int? Version { get; set; }


        [NotMapped]
        public new string Description { get; set; }
        public virtual Move Moves { get; set; }
        
        //public new virtual ICollection<MoveData_name> NameSet { get; set; }
        //public virtual HitType counterType_codeNavigation { get; set; }
        //public virtual HitType guardType_codeNavigation { get; set; }
        //public virtual HitType hitType_codeNavigation { get; set; }
        //public virtual MoveSubType moveSubType_codeNavigation { get; set; }
        //public virtual MoveType moveType_codeNavigation { get; set; }
        //public virtual HitType startType_codeNavigation { get; set; }
        //public virtual ICollection<MoveData_name> Move_Data_Name { get; set; }
    }
}