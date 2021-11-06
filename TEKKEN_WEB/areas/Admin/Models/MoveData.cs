using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEKKEN_WEB.Models
{
    public class MoveData : BaseModel
    {
        [Display(Name = "Move_Code")]
        [Column(TypeName = "decimal(9, 0)")]
        [Required(ErrorMessage = "코드를 입력해 주세요.")]
        public int Move_Code { get; set; }
        [Display(Name = "기술명")]
        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "기술명을 입력해 주세요.")]
        public string Move_Name { get; set; }

        //[Display(Name = "커맨드")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "커맨드를 입력해 주세요.")]
        //public string Command { get; set; }

        public int Data_id { get; set; }


        [Display(Name = "기술 종류")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "MoveType_code를 입력해 주세요.")]
        public int MoveType_code { get; set; }

        [Display(Name = "상태명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "이름 입력해 주세요.")]
        public string MoveType_Name { get; set; }

        [Display(Name = "기술 하위 분류")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "SubType_code 입력해 주세요.")]
        public int MoveSubType_code { get; set; }

        [Display(Name = "상태명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "상태를 입력해 주세요.")]
        public string MoveSubType_Name { get; set; }

        [Required(ErrorMessage = "코드를 입력해 주세요.")]
        public int HitCount { get; set; }
        public string HitLevel { get; set; }
        public int Damage { get; set; }
        public int StartFrame { get; set; }
        
        [Display(Name = "Start Display")]
        public string StartFrame_Display { get; set; }
        [Display(Name = "Start Type")]
        public int StartType_code { get; set; }
        
        [Display(Name = "Guard Frame")]
        public int GuardFrame { get; set; }
        
        [Display(Name = "Guard Display")]
        public string GuardFrame_Display { get; set; }
        [Display(Name = "Guard Type")]
        public int GuardType_code { get; set; }

        [Display(Name = "Hit Frame")]
        public int HitFrame { get; set; }
        [Display(Name = "Hit Display")]
        public string HitFrame_Display { get; set; }
        [Display(Name = "Hit Type")]
        public int HitType_code { get; set; }
        [Display(Name = "Counter Frame")]
        public int CounterFrame { get; set; }
        [Display(Name = "Counter Display")]
        public string CounterFrame_Display { get; set; }
        [Display(Name = "Counter Type")]
        public int CounterType_code { get; set; }
        public string BreakThrow { get; set; }
        public string AfterBreak { get; set; }
        public string StartType_name { get; set; }
        public string Guardtype_name { get; set; }
        public string HitType_name { get; set; }
        public string CounterType_name { get; set; }


        [Display(Name = "Homing")]
        public bool Homing { get; set; }
        [Display(Name = "PowerCrush")]
        public bool PowerCrush { get; set; }
        [Display(Name = "TechnicallyCrouching")]
        public bool TechnicallyCrouching { get; set; }
        [Display(Name = "TechnicallyJumping")]
        public bool TechnicallyJumping { get; set; }
        [Display(Name = "TailSpin")]
        public bool TailSpin { get; set; }
        [Display(Name = "WallSplat")]
        public bool WallSplat { get; set; }


        [Display(Name = "변경")]
        public bool Change { get; set; }

    }
}

