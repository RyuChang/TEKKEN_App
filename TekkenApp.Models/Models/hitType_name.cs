#nullable disable

namespace TekkenApp.Models
{
    public partial class HitType_name : BaseNameEntity
    {
        public HitType_name()
        {
            SetApp(TableName.HitType_name);
        }

        public virtual HitType hitType_codeNavigation { get; set; }
    }
}