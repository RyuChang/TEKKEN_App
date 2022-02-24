using System.Text.RegularExpressions;

namespace NewTekkenApp.Utilities
{
    public class CommandLibrary
    {
        public static string TranseCommandToImage(String command)
        {
            var result = $"<img class=\"move\" src=\"/images/[C].svg\" />";
            return Regex.Replace(command, @"\[(\S+?)\]", m => result.Replace("[C]", m.Value.Replace("[", "").Replace("]", "")), RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
    }
}
