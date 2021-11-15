using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TekkenApp.Models
{
    [Index(nameof(code), nameof(number), Name = "IX_moveType_1", IsUnique = true)]
    [Index(nameof(code), Name = "IX_moveType_code_unique", IsUnique = true)]
    public partial class MoveType
    {
        public MoveType()
        {
            moveType_name = new HashSet<MoveType_name>();
            move_data = new HashSet<Move_data>();
        }

        [Key]
        public int id { get; set; }
        public int code { get; set; }
        public byte number { get; set; }
        [Required]
        public string description { get; set; }

        public virtual ICollection<MoveType_name> moveType_name { get; set; }
        public virtual ICollection<Move_data> move_data { get; set; }
    }
}