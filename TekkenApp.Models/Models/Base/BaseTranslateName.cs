using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekkenApp.Models
{
    [NotMapped]
    public abstract class BaseTranslateName //: BaseEntity
    {
        [NotMapped]
        public string preUrl { get; set; }

        protected TableName tableName { get; set; }

        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "ID를 입력해 주세요.")]
        public int Id { get; set; }


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
        protected void SetApp(TableName tableName)
        {
            this.tableName = tableName;
            preUrl = $"/{tableName.ToString().Replace("_name", "") + "s"}";

        }
    }
}
