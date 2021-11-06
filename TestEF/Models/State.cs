﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Models.Models
{
    [Index(nameof(Code), Name = "IX_State", IsUnique = true)]
    [Index(nameof(StateGroupCode), nameof(Number), Name = "IX_State_1", IsUnique = true)]
    public partial class State
    {
        public State()
        {
            StateName = new HashSet<StateName>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("code")]
        public int Code { get; set; }
        [Column("number")]
        public byte Number { get; set; }
        [Column("StateGroup_code")]
        public int StateGroupCode { get; set; }
        [Required]
        [Column("description")]
        public string Description { get; set; }

        public virtual StateGroup StateGroupCodeNavigation { get; set; }
        public virtual ICollection<StateName> StateName { get; set; }
    }
}