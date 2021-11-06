using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEKKEN_WEB.Models
{
    public class TekkenVersion
    {
        [Display(Name = "버전")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "* 버전을 작성해 주세요.")]
        public float Version { get; set; }

        [Display(Name = "시즌")]
        [Column(TypeName = "decimal(1, 0)")]
        [Required(ErrorMessage = "시즌을 작성해 주세요.")]
        public Decimal Season { get; set; }

        [Display(Name = "수정일")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "날짜를 작성해 주세요.")]
        //[DisplayFormat( DataFormatString = "{0:d}")]
        public DateTime UpdateDate { get; set; }
    }
}
