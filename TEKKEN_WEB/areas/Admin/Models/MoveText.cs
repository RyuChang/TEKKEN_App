using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TEKKEN_WEB.Models
{
    public class MoveText : BaseModel
    {
        [Display(Name = "캐릭터 코드")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "캐릭터 코드를 입력해 주세요.")]
        public int Character_code { get; set; }
    }
}
