﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TekkenApp.Models
{
    [Index(nameof(character_code), nameof(number), Name = "IX_moveSubType_character_code_number", IsUnique = true)]
    [Index(nameof(code), Name = "IX_moveSubType_code", IsUnique = true)]
    public partial class MoveSubType
    {
        public MoveSubType()
        {
            moveSubType_name = new HashSet<MoveSubType_name>();
            move_data = new HashSet<Move_data>();
        }

        [Key]
        public int id { get; set; }
        public int code { get; set; }
        public byte character_code { get; set; }
        public byte number { get; set; }
        [Required]
        public string description { get; set; }

        public virtual Character character_codeNavigation { get; set; }
        public virtual ICollection<MoveSubType_name> moveSubType_name { get; set; }
        public virtual ICollection<Move_data> move_data { get; set; }
    }
}