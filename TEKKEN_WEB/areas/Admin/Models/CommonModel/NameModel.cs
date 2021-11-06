using System.ComponentModel.DataAnnotations;

namespace TEKKEN_WEB.Models
{
    public class NameModel
    {
        [Display(Name = "입력 언어")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "입력 언어를 입력해 주세요.")]
        public string Language_Code { get; set; }
   
        [Display(Name = "명칭")]
        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "이름 입력해 주세요.")]
        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}