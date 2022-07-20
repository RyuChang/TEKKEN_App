using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;
using TekkenApp.Models.Models.Maps;

namespace TekkenApp.Data
{
    /// <summary>
    /// 키와 코드를 입력 받아서, 시스템에 있는 값으로 매핑해주는  클래스
    /// </summary>
    public class CommanderMapperService : ICommanderMapperService
    {
        Dictionary<string, string> KeyMap { get; set; }
        Dictionary<int, StateGroupMap> StateGroupMap { get; set; }
        Dictionary<int, StateMap> StateMap { get; set; }
        Dictionary<string, string> TextMap { get; set; }
        TekkenDbContext TekkenDbContext { get; set; }


        public CommanderMapperService(TekkenDbContext _tekkenDbContext)
        {
            TekkenDbContext = _tekkenDbContext;
            InitMaps();
        }

        /// <summary>
        /// 각 키 맵들을 초기화
        /// </summary>
        private void InitMaps()
        {
            StateGroupMap = new Dictionary<int, StateGroupMap>();
            StateMap = new Dictionary<int, StateMap>();
            SetKeyMaps();
            SetStateGroups();
            SetState();
        }


        /// <summary>
        /// 키보드맵 초기화
        /// </summary>
        private void SetKeyMaps()
        {
            KeyMap = TekkenDbContext.Command.Select(p => new { p.key, p.CommandCode })
            .AsEnumerable()
            .ToDictionary(k => k.key, v => v.CommandCode);
        }

        /// <summary>
        /// StateGroup맵 초기화
        /// </summary>
        private void SetStateGroups()
        {
            IList<StateGroup> StateGroupList = TekkenDbContext.StateGroup.Include(s => s.NameSet).ToList();

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

        /// <summary>
        /// State맵 초기화
        /// </summary>
        private void SetState()
        {
            IList<State> StateList = TekkenDbContext.State.Include(s => s.NameSet).ToList();

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

        /// <summary>
        /// 키보드 입력값 매핑
        /// </summary>
        /// <param name="formedKey">입력받은 키</param>
        /// <returns>매핑된 키 값</returns>
        public string MapKey(string formedKey)
        {
            string key = string.Empty;

            if (KeyMap.TryGetValue(formedKey, out key))
            {
                return $"[{key.ToString().Trim()}]";
            }
            return key;
        }

        /// <summary>
        /// state 입력값 매핑
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="languageCode"></param>
        /// <returns>매핑된 state값</returns>
        public string MapState(string stateCode, string languageCode)
        {
            return StateMap[int.Parse(stateCode)].name[languageCode];
        }
    }
}
