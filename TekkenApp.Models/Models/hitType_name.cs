using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TekkenApp.Models
{
    public partial class HitType_name : BaseTranslateName
    {
        [NotMapped]
        public static APP APP_TYPE = APP.HitType;
        [NotMapped]
        public static string PRE_URL = APP_TYPE.ToString();

        public HitType_name()
        {
            SetApp(TableName.HitType_name);
        }
        
        //[Key]
        //[Display(Name = "ID")]
        ////[Column(TypeName = "decimal(3, 0)")]
        //[Required(ErrorMessage = "ID를 입력해 주세요.")]
        //public int Id { get; set; }

        //[Display(Name = "코드")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "코드를 입력해 주세요.")]
        //public int Base_code { get; set; }

        //[Display(Name = "입력 언어")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "입력 언어를 입력해 주세요.")]
        //[StringLength(2)]
        //public string Language_code { get; set; }

        //[Display(Name = "번역명")]
        //[DataType(DataType.Text)]
        //public string Name { get; set; }

        //[Column("checked")]
        //public bool Checked { get; set; }


        public virtual HitType hitType_codeNavigation { get; set; }

        //public string GetTableName()
        //{
        //    return tableName.ToString();
        //}
    }
}