using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;
using TekkenApp.Models.Models.Maps;

namespace TekkenApp.Data
{
    public class CommanderMapperService : ICommanderMapperService
    {
        Dictionary<string, string> KeyMap { get; set; }
        Dictionary<int, StateGroupMap> StateGroupMap { get; set; }
        Dictionary<int, StateMap> StateMap { get; set; }
        Dictionary<string, string> TextMap { get; set; }
        TekkenDbContext TekkenDbContext { get; set; }
        public CommanderMapperService()
        {
            TekkenDbContext = new TekkenDbContext();
            SetMaps();

        }
        private async Task SetMaps()
        {
            StateGroupMap = new Dictionary<int, StateGroupMap>();
            StateMap = new Dictionary<int, StateMap>();
            SetKeyMaps();
            await SetStateGroups();
            await SetState();

        }
        private void SetKeyMaps()
        {
            KeyMap = TekkenDbContext.Command.Select(p => new { p.key, p.CommandCode })
            .AsEnumerable()
            .ToDictionary(k => k.key, v => v.CommandCode);
        }

        private async Task SetStateGroups()
        {
            IList<StateGroup> StateGroupList = await TekkenDbContext.StateGroup.Include(s => s.NameSet).ToListAsync();

            foreach (StateGroup s in StateGroupList)
            {
                StateGroupMap stateGroupMap = new StateGroupMap();

                foreach (StateGroup_name name in s.NameSet)
                {
                    stateGroupMap.name.Add(name.Language_code, name.Name);
                }

                StateGroupMap.Add(s.Code, stateGroupMap);
            }
        }

        private async Task SetState()
        {
            IList<State> StateList = await TekkenDbContext.State.Include(s => s.NameSet).ToListAsync();

            foreach (State s in StateList)
            {
                StateMap stateMap = new StateMap();

                foreach (State_name name in s.NameSet)
                {
                    stateMap.name.Add(name.Language_code, name.Name);
                }

                StateMap.Add(s.Code, stateMap);
            }
        }

        public string MapKey(string formedKey)
        {
            string key = string.Empty;

            if (KeyMap.TryGetValue(formedKey, out key))
            {
                return $"[{key.ToString().Trim()}]";
            }
            return key;
        }

        public string MapState(string stateCode, string languageCode)
        {
            return StateMap[int.Parse(stateCode)].name[languageCode];
        }

        //public Dictionary<string, string> GetKeyMaps()
        //{
        //    return KeyMap;
        //}
    }
}
