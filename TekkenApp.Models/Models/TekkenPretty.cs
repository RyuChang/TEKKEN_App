using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Humanizer.On;

namespace TekkenApp.Models
{
    public class TekkenPretty
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "ID를 입력해 주세요.")]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Character_Name { get; set; }
        public string Language_code { get; set; }

        public string Title { get; set; }
        public string Hit { get; set; }
        public string Command { get; set; }
        public string HitLv { get; set; }
        public string Dmg { get; set; }
        public string DisplayDmg { get; set; }
        public string StartFrame { get; set; }
        public string StratSegFrame { get; set; }
        public string BlockFrame { get; set; }
        public string HitFrame { get; set; }
    }
}
