using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TekkenApp.Models
{
    [Index(nameof(character_code), nameof(number), Name = "IX_moveText_character_code_number", IsUnique = true)]
    [Index(nameof(code), Name = "IX_moveText_code", IsUnique = true)]
    public partial class MoveText
    {
        public MoveText()
        {
            moveText_name = new HashSet<MoveText_name>();
        }

        [Key]
        public int id { get; set; }
        public int code { get; set; }
        public byte character_code { get; set; }
        public byte number { get; set; }
        [Required]
        public string description { get; set; }

        public virtual Character character_codeNavigation { get; set; }
        public virtual ICollection<MoveText_name> moveText_name { get; set; }
    }
}