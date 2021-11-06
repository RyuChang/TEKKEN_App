using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TEKKEN_WEB.Models
{
    public class Character : BaseModel
    {
        [Display(Name = "시즌")]
        [Column(TypeName = "decimal(1, 0)")]
        [Required(ErrorMessage = "코드를 입력해 주세요.")]
        public int Season { get; set; }

        [Display(Name = "전체 캐릭명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "전체이름 입력해 주세요.")]
        public string FullName { get; set; }
    }
}

