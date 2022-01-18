using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TekkenApp.Models
{
    [NotMapped]
    
    public class BaseDataEntity
    {
        [NotMapped]
        public static string preUrl { get; set; }
        protected TableName tableName { get; set; }

        [NotMapped]
        public int StateGroup_code { get; set; }

        [NotMapped]
        public int Character_code { get; set; }

        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "ID를 입력해 주세요.")]
        public int Id { get; set; }

        public int Code { get; set; }

        public int Number { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<BaseNameEntity> NameSet { get; set; }

        protected void SetApp(TableName tableName)
        {
            this.tableName = tableName;
            preUrl = $"/{tableName.ToString().Replace("_name", "") + "s"}";
        }

        public static string GetPreUrl()
        {
            return preUrl;
        }
    }
}