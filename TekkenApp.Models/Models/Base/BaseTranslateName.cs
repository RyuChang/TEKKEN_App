using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekkenApp.Models
{
    [NotMapped]
    public abstract class BaseTranslateName : BaseEntity
    {
        
        public BaseTranslateName()
        {

        }

        public BaseTranslateName(int id, int code, string language_code, string name, bool _checked)
        {
            this.Id = id;
            this.Base_code = code;
            this.Language_code = language_code;
            this.Name = name;
            this.Checked = _checked;
        }



        
        [Display(Name = "코드")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "코드를 입력해 주세요.")]
        public int Base_code { get; set; }

        [Display(Name = "입력 언어")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "입력 언어를 입력해 주세요.")]
        [StringLength(2)]
        public string Language_code { get; set; }

        [Display(Name = "번역명")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Column("checked")]
        public bool Checked { get; set; }

        public string GetTableName()
        {
            return tableName.ToString();
        }
    }
}
