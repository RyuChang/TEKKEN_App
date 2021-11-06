﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Models.Models
{
    [Table("move_command_name")]
    [Index(nameof(LanguageCode), nameof(MoveCommandCode), Name = "IX_move_command", IsUnique = true)]
    public partial class MoveCommandName
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("Move_Command_code")]
        public int MoveCommandCode { get; set; }
        [Required]
        [Column("language_code")]
        [StringLength(2)]
        public string LanguageCode { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("checked")]
        public bool Checked { get; set; }

        public virtual Language LanguageCodeNavigation { get; set; }
        public virtual Move MoveCommandCodeNavigation { get; set; }
    }
}