using Google.Apis.YouTube.v3.Data;
using System;

namespace TekkenApp.Models
{
    public partial class MigrationDataVM
    {
        public int number { get; set; }
        public string description { get; set; }
        public string TITLE_EN { get; set; }
        public string hit { get; set; }
        public string HitLv { get; set; }
        public string Dmg { get; set; }
        public string DisplayDmg { get; set; }
        public string StartFrame { get; set; }
        public string BlockFrame { get; set; }
        public string HitFrame { get; set; }
        public string CounterFrame { get; set; }

    }
}