﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Models.Models
{
    [Index(nameof(Code), Name = "IX_StateGroup", IsUnique = true)]
    [Index(nameof(Number), Name = "IX_StateGroup_1", IsUnique = true)]
    public partial class StateGroup
    {
        public StateGroup()
        {
            State = new HashSet<State>();
            StateGroupName = new HashSet<StateGroupName>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("code")]
        public int Code { get; set; }
        [Required]
        [Column("description")]
        public string Description { get; set; }
        [Column("number")]
        public byte Number { get; set; }

        public virtual ICollection<State> State { get; set; }
        public virtual ICollection<StateGroupName> StateGroupName { get; set; }
    }
}