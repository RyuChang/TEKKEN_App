using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEKKEN_WEB.Models
{
    public class LastInfo
    {
        [Display(Name = "코드")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "코드를 입력해 주세요.")]
        public string Character { get; set; }

        [Display(Name = "코드")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "코드를 입력해 주세요.")]
        public int Code { get; set; }

        [Display(Name = "테이블명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "테이블이름 입력해 주세요.")]
        public string TableName { get; set; }
    }
}