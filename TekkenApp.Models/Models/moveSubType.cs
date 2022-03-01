﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TekkenApp.Models
{
    //[Index(nameof(character_code), nameof(number), Name = "IX_moveSubType_character_code_number", IsUnique = true)]
    //[Index(nameof(code), Name = "IX_moveSubType_code", IsUnique = true)]
    public partial class MoveSubType : BaseDataEntity
    {
        public MoveSubType()
        {
            SetApp(TableName.MoveSubType);
        }

        public new int Character_code
        {
            get
            {
                return base.Character_code;
            }
            set
            {
                base.Character_code = value;
            }
        }
        //[Key]
        //public int id { get; set; }
        //public int code { get; set; }
        //public byte character_code { get; set; }
        //public byte number { get; set; }
        //[Required]
        //public string description { get; set; }

        //public virtual Character character_codeNavigation { get; set; }
        //public virtual ICollection<MoveSubType_name> moveSubType_name { get; set; }
        //public virtual ICollection<Move_data> move_data { get; set; }
    }
}