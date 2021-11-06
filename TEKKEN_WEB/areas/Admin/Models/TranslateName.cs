using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TEKKEN_WEB.Enums;

namespace TEKKEN_WEB.Models
{
    public class TranslateName
    {
        public TranslateName()
        {
        }
        public TranslateName(TableName tableName, int id, int code, string name, string language_Code, bool change)
        {
            this.TableName = tableName;
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Language_Code = language_Code;
            this.Change = change;
        }

        [Display(Name = "ID")]
        [Column(TypeName = "decimal(3, 0)")]
        [Required(ErrorMessage = "ID를 입력해 주세요.")]
        public int Id { get; set; }

        [Display(Name = "코드")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "코드를 입력해 주세요.")]
        public int Code { get; set; }

        [Display(Name = "입력 언어")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "입력 언어를 입력해 주세요.")]
        public string Language_Code { get; set; }

        [Display(Name = "설명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "설명를 입력해 주세요.")]
        public string Description { get; set; }

        [Display(Name = "번역명")]
        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "이름 입력해 주세요.")]
        public string Name { get; set; }

        [Display(Name = "테이블명")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "테이블이름 입력해 주세요.")]
        public TableName TableName { get; set; }

        [Display(Name = "변경")]
        public bool Change { get; set; }
        //[Display(Name = "전체 캐릭명")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "전체이름 입력해 주세요.")]
        //public string FullName { get; set; }
    }
}