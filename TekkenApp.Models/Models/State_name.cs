﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TekkenApp.Models
{
    //[Index(nameof(state_code), nameof(language_code), Name = "IX_State_name", IsUnique = true)]
    public partial class State_name : BaseNameEntity
    {
        public virtual Language language_codeNavigation { get; set; }
        public virtual State state_codeNavigation { get; set; }
    }
}