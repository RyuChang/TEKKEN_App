﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TekkenApp.Models
{
    //[Index(nameof(Base_code), nameof(language_code), Name = "IX_StateGroup_name", IsUnique = true)]
    public partial class StateGroup_name : BaseTranslateName
    {
        public StateGroup_name()
        {
            tableName = TableName.StateGroup_name;
        }
        //[Key]
        //public int id { get; set; }
        //public int StateGroup_code { get; set; }
        //[Required]
        //[StringLength(2)]
        //public string language_code { get; set; }

        //[Required]
        //public string name { get; set; }
        //public bool Checked { get; set; }

        public virtual StateGroup StateGroup_codeNavigation { get; set; }
        public virtual Language language_codeNavigation { get; set; }
    }
}