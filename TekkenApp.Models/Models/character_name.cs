using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekkenApp.Models
{
    //[Index(nameof(character_code), nameof(language_code), Name = "IX_character_name", IsUnique = true)]
    public partial class Character_name : BaseNameEntity
    {
        public Character_name()
        {
            SetApp(TableName.Character_name);
        }

        [Required]
        public string fullName { get; set; }

        [NotMapped]
        public string Checked { get; set; }

        

    }
}