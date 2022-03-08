using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface ICommandService : IBaseNameService<Command, Command_name>
    {
        void InitCommand(string rawCommand);
        void AddKey(string key);
        bool RemoveKey(string key);
        string GetRawCommand();
        string GetDisplayCommand();
        void AddState(string stateGroupType, int stateCode, int dataCode = 0);
        Task SetCommand();
        Task<string> TransCommand(string rawCommand, string language_code);
        string GetStateGroupType(int stateGroupCode);
    }
}