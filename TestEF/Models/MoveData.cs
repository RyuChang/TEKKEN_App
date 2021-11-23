﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Models.Models
{
    [Table("move_data")]
    [Index(nameof(MoveCode), Name = "IX_move_data", IsUnique = true)]
    public partial class MoveData
    {
        public MoveData()
        {
            MoveDataName = new HashSet<MoveDataName>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("Move_Code")]
        public int MoveCode { get; set; }
        [Column("moveType_code")]
        public int? MoveTypeCode { get; set; }
        [Column("moveSubType_code")]
        public int? MoveSubTypeCode { get; set; }
        [Column("hitCount")]
        public byte HitCount { get; set; }
        [Required]
        [Column("hitLevel")]
        public string HitLevel { get; set; }
        [Column("damage")]
        public short Damage { get; set; }
        [Column("startFrame")]
        public short StartFrame { get; set; }
        [Column("startFrame_Display")]
        public string StartFrameDisplay { get; set; }
        [Column("startType_code")]
        public int? StartTypeCode { get; set; }
        [Column("guardFrame")]
        public short GuardFrame { get; set; }
        [Column("guardFrame_Display")]
        public string GuardFrameDisplay { get; set; }
        [Column("guardType_code")]
        public int GuardTypeCode { get; set; }
        [Column("hitFrame")]
        public short HitFrame { get; set; }
        [Column("hitFrame_Display")]
        public string HitFrameDisplay { get; set; }
        [Column("hitType_code")]
        public int HitTypeCode { get; set; }
        [Column("counterFrame")]
        public short CounterFrame { get; set; }
        [Column("counterFrame_Display")]
        public string CounterFrameDisplay { get; set; }
        [Column("counterType_code")]
        public int CounterTypeCode { get; set; }
        [Column("breakThrow")]
        public string BreakThrow { get; set; }
        [Column("afterBreak")]
        public string AfterBreak { get; set; }
        [Column("homing")]
        public bool Homing { get; set; }
        [Column("powerCrush")]
        public bool PowerCrush { get; set; }
        [Column("technicallyCrouching")]
        public bool TechnicallyCrouching { get; set; }
        [Column("technicallyJumping")]
        public bool TechnicallyJumping { get; set; }
        [Column("tailSpin")]
        public bool TailSpin { get; set; }
        [Column("wallSplat")]
        public bool WallSplat { get; set; }
        [Column("note")]
        public string Note { get; set; }
        [Column("version", TypeName = "decimal(4, 2)")]
        public decimal? Version { get; set; }

        public virtual HitType CounterTypeCodeNavigation { get; set; }
        public virtual HitType GuardTypeCodeNavigation { get; set; }
        public virtual HitType HitTypeCodeNavigation { get; set; }
        public virtual Move MoveCodeNavigation { get; set; }
        public virtual MoveSubType MoveSubTypeCodeNavigation { get; set; }
        public virtual MoveType MoveTypeCodeNavigation { get; set; }
        public virtual HitType StartTypeCodeNavigation { get; set; }
        public virtual ICollection<MoveDataName> MoveDataName { get; set; }
    }
}