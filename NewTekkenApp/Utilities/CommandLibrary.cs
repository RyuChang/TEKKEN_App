using System.Text.RegularExpressions;

namespace NewTekkenApp.Utilities
{
    public class CommandLibrary
    {
        public static string TranseCommandToImage(String command)
        {
            string displayCommand = command.Replace("[NL]", "<BR>").Replace("/", " ");
            var result = $"<img class=\"move\" src=\"/images/[C].svg\" />";
            return Regex.Replace(displayCommand, @"\[(\S+?)\]", m => result.Replace("[C]", m.Value.Replace("[", "").Replace("]", "")), RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
    }
}
