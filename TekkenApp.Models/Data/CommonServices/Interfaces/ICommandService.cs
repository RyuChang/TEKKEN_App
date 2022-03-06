using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface ICommandService : IBaseNameService<Command, Command_name>
    {
        void AddKey(string key);
        string GetRawCommand();
        string GetDisplayCommand();
        void InitCommand(string rawCommand);
        void RemoveKey(string key);
        Task SetCommand(string result);
        Task<string> TransCommand(string rawCommand, string language_code);
        Task AddState(string stateGroupType, int stateCode, int dataCode = 0);
        string GetStateGroupType(int stateGroupCode);
    }
}