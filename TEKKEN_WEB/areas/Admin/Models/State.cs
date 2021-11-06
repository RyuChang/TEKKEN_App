using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEKKEN_WEB.Models
{
    public class State : BaseModel
    {
        [Display(Name = "StateGroup_code")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "StateGroup을 입력해 주세요.")]
        public int StateGroup_code { get; set; }

        [Display(Name = "상태명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "상태명 입력해 주세요.")]
        public string StateGroup_name { get; set; }
    }
}