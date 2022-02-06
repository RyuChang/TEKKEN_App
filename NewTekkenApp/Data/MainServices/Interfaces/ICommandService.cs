using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface ICommandService : IBaseService<Command, Command_name>
    {
        Task<List<KeyMapVM>> GetKeyMaps();
    }
}