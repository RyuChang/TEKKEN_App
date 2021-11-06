using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEKKEN_WEB.Models
{
    public class MoveCommand : BaseModel
    {
        [Display(Name = "기술명")]
        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "기술명을 입력해 주세요.")]
        public string Move_Name { get; set; }

        [Display(Name = "커맨드")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "커맨드를 입력해 주세요.")]
        public string Command { get; set; }

 
        [Display(Name = "변경")]
        public bool Change { get; set; }

    }
}