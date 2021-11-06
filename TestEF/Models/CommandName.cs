﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Models.Models
{
    [Table("command_name")]
    [Index(nameof(CommandCode), nameof(LanguageCode), Name = "IX_command_name", IsUnique = true)]
    public partial class CommandName
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("command_code")]
        [StringLength(3)]
        public string CommandCode { get; set; }
        [Required]
        [Column("language_code")]
        [StringLength(2)]
        public string LanguageCode { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }

        public virtual Language LanguageCodeNavigation { get; set; }
    }
}