using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEKKEN_WEB.Models
{
    public class BaseModel : NameModel
    {
        [Key]
        [Display(Name = "ID")]
        [Column(TypeName = "decimal(9, 0)")]
        [Required(ErrorMessage = "ID를 입력해 주세요.")]
        public int Id { get; set; }

        [Display(Name = "캐릭터 코드")]
        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "캐릭터 코드를 입력해 주세요.")]
        public int Character_code { get; set; }

        [Display(Name = "StateGroup_code")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "StateGroup을 입력해 주세요.")]
        public string StateGroup_code { get; set; }

        [Display(Name = "코드")]
        [Column(TypeName = "decimal(9, 0)")]
        [Required(ErrorMessage = "코드를 입력해 주세요.")]
        public int Code { get; set; }

        [Display(Name = "순번")]
        [Column(TypeName = "decimal(3, 0)")]
        [Required(ErrorMessage = "번호를 입력해 주세요.")]
        public int Number { get; set; }

        [Display(Name = "설명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "설명를 입력해 주세요.")]
        public string Description { get; set; }
    }
}