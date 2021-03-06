// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Models.Models
{
    [Table("character")]
    [Index(nameof(Code), Name = "IX_character", IsUnique = true)]
    [Index(nameof(CodeName), Name = "IX_character_code_Unique", IsUnique = true)]
    public partial class Character
    {
        public Character()
        {
            Move = new HashSet<Move>();
            MoveSubType = new HashSet<MoveSubType>();
            MoveText = new HashSet<MoveText>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("code")]
        public byte Code { get; set; }
        [Required]
        [Column("code_name")]
        [StringLength(3)]
        public string CodeName { get; set; }
        [Column("season")]
        public byte Season { get; set; }
        [Required]
        [Column("description")]
        public string Description { get; set; }

        public virtual ICollection<Move> Move { get; set; }
        public virtual ICollection<MoveSubType> MoveSubType { get; set; }
        public virtual ICollection<MoveText> MoveText { get; set; }
    }
}