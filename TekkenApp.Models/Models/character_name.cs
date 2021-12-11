using System.ComponentModel.DataAnnotations;

namespace TekkenApp.Models
{
    //[Index(nameof(character_code), nameof(language_code), Name = "IX_character_name", IsUnique = true)]
    public partial class Character_name : BaseNameEntity
    {
        public Character_name()
        {
            SetApp(TableName.Character_name);
        }
        public int character_code { get; set; }

        [Required]
        public string fullName { get; set; }

    }
}