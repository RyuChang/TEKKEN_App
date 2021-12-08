using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TekkenApp.Models
{
    public partial class HitType_name : BaseNameEntity
    {
        //[NotMapped]
        //public static AppType APP_TYPE = AppType.HitTypes;
        //[NotMapped]
        //public static string PRE_URL = APP_TYPE.ToString();

        public HitType_name()
        {
            SetApp(TableName.HitType_name);
        }

        public virtual HitType HitType { get; set; }
    }
}