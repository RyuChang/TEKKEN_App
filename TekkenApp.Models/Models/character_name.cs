﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TekkenApp.Models
{
    [Index(nameof(character_code), nameof(language_code), Name = "IX_character_name", IsUnique = true)]
    public partial class character_name
    {
        [Key]
        public int id { get; set; }
        public int character_code { get; set; }
        [Required]
        [StringLength(2)]
        public string language_code { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string fullName { get; set; }

        public virtual language language_codeNavigation { get; set; }
    }
}