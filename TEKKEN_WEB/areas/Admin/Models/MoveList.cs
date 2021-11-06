using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEKKEN_WEB.Models
{
    public class MoveList : MoveData 
    {
        [Display(Name = "캐릭터 코드")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "캐릭터 코드를 입력해 주세요.")]
        public int Character_code { get; set; }

        [Display(Name = "캐릭터 명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "캐릭터명을 입력해 주세요.")]
        public string Character_Name { get; set; }


        public string Command { get; set; }
        //[Display(Name = "순번")]
        //[Column(TypeName = "decimal(3, 0)")]
        //[Required(ErrorMessage = "번호를 입력해 주세요.")]
        //public decimal Number { get; set; }



        //[Display(Name = "설명")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "설명를 입력해 주세요.")]
        //public string Description { get; set; }



        [Display(Name = "버전")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "* 버전을 작성해 주세요.")]
        public float Version { get; set; }

        //[Display(Name = "기술명")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "기술명을 입력해 주세요.")]
        //public string Name { get; set; }
    }
}