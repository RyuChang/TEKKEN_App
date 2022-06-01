using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class CommandService : BaseNameService<Command, Command_name>, ICommandService
    {
        private IStateService StateService { get; set; }
        private IMoveService MoveService { get; set; }
        private IMoveTextService MoveTextService { get; set; }
        private IMoveSubTypeService MoveSubTypeService { get; set; }
        private ICommanderMapperService CommanderMapperService { get; set; }
        private int Timer { get; set; }
        private string RawCommand { get; set; }
        private string DisplayCommand { get; set; }
        private List<string> resultKey { get; set; }
        private List<string> clickedKey { get; set; }


        public CommandService(TekkenDbContext tekkenDbContext, ICommanderMapperService _commanderMapperService, IStateService _stateService, IMoveService _moveService, IMoveTextService _moveTextService, IMoveSubTypeService _moveSubTypeService) : base(tekkenDbContext, tekkenDbContext.Command, tekkenDbContext.Command_name)
        {
            MainTable = TableName.Command.ToString();
            NameTable = TableName.Command_name.ToString();

            this.StateService = _stateService;
            this.MoveService = _moveService;
            this.MoveTextService = _moveTextService;
            this.MoveSubTypeService = _moveSubTypeService;

            this.CommanderMapperService = _commanderMapperService;
        }

        #region Key 입력 이벤트 처리
        public void InitCommand(string rawCommand)
        {
            this.RawCommand = rawCommand;
            clickedKey = new List<string>();
            resultKey = new List<string>();
        }

        public void AddKey(string key)
        {
            Timer++;
            if (!clickedKey.Contains(key))
            {
                clickedKey.Add(key);
                resultKey.Add(key);
            }
        }

        public void AddCommand(string key)
        {
            string result = RawCommand;
            string formedKey = String.Empty;

            if (key == "Backspace")
            {
                int lastIndex = RawCommand.LastIndexOf("/");
                if (lastIndex < 0) { lastIndex = 0; }
                result = RawCommand.Substring(0, lastIndex);
            }
            else if (clickedKey.Count == 1)
            {
                formedKey = $"{(Timer > 1 ? "L" : "")}{key.ToUpper()}";
            }
            else if (clickedKey.Count >= 2)
            {
                clickedKey.Sort();
                formedKey = String.Join("+", clickedKey.ToArray()).ToUpper();
            }

            string mapppedKey = CommanderMapperService.MapKey(formedKey);
            if (!string.IsNullOrEmpty(mapppedKey))
            {
                result += '/' + mapppedKey;
            }

            RawCommand = result;
        }

        public bool RemoveKey(string key)
        {
            bool isExist = this.resultKey.Contains(key);
            if (isExist)
            {
                resultKey.Remove(key);
            }

            if (resultKey.Count == 0)
            {
                AddCommand(key);
                return true;
            }
            return false;
        }

        private void ClearCommand()
        {
            clickedKey.Clear();
            Timer = 0;
        }
        #endregion


        public async Task SetCommand()
        {
            if (RawCommand.Length > 0 && RawCommand[0] == '/')
            {
                RawCommand = RawCommand.Substring(1, RawCommand.Length - 1);
            }

            DisplayCommand = await TransCommand(RawCommand, "en");
            ClearCommand();
        }

        public string GetRawCommand()
        {
            return RawCommand;
        }

        public string GetDisplayCommand()
        {
            return DisplayCommand;
        }

        public void AddState(string stateGroupType, int stateCode, int dataCode = 0)
        {
            RawCommand = $"{RawCommand}/{{{stateGroupType}:{stateCode}{(dataCode == 0 ? "" : $":{dataCode}")}}}";
        }


        public async Task<string> TransCommand(string rawCommand, string language_code)
        {
            string[] arrayCommand = rawCommand.Split('/');
            string ResultCommand = string.Empty;

            foreach (string cmd in arrayCommand)
            {
                CommandDetail commandDetail = GetCommand(cmd);
                string result = await Trans(commandDetail, language_code);

                ResultCommand += result + " ";
            }

            return TranseCommandToImage(ResultCommand);
        }

        public String TranseCommandToImage(String command)
        {
            string displayCommand = command.Replace("[NL]", "<BR>").Replace("/", " ");
            var result = $"<img class=\"move\" src=\"/images/[C].svg\" />";
            return Regex.Replace(displayCommand, @"\[(\S+?)\]", m => result.Replace("[C]", m.Value.Replace("[", "").Replace("]", "")), RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }


        internal async Task<string> Trans(CommandDetail commandInfo, string language_code)
        {

            string result = "";
            if (commandInfo.Type == "C")
            {
                result = commandInfo.Command;
            }
            else if (commandInfo.Type == "T")
            {
                string moveText = await GetMoveText(commandInfo.Data, language_code);
                string stateName = CommanderMapperService.MapState(commandInfo.Command, language_code);
                result = stateName.Replace("{TEXT}", moveText);
            }
            else if (commandInfo.Type == "M")
            {
                string move = await GetMove(commandInfo.Data, language_code);
                string stateName = CommanderMapperService.MapState(commandInfo.Command, language_code);
                result = stateName.Replace("{MOVE}", move);
            }
            else if (commandInfo.Type == "U")
            {
                string moveSubTypeText = await GetMoveSubType(commandInfo.Data, language_code);
                string stateName = CommanderMapperService.MapState(commandInfo.Command, language_code);
                result = stateName.Replace("{TEXT}", moveSubTypeText);
            }
            else if (commandInfo.Type == "S")
            {
                result = CommanderMapperService.MapState(commandInfo.Command, language_code);
            }

            return result;
        }


        public async Task<string> GetMoveText(string code, string language_code = "en")
        {
            // 없을 경우 예외 처리 필요
            MoveText_name moveText_Name = await MoveTextService.GetNameEntitiyByBaseCodeAndLanguageCode(int.Parse(code), language_code);
            return moveText_Name.Name;
        }
        public async Task<string> GetMove(string code, string language_code = "en")
        {
            // 없을 경우 예외 처리 필요
            Move_name move_Name = await MoveService.GetNameEntitiyByBaseCodeAndLanguageCode(int.Parse(code), language_code);
            return move_Name.Name;
        }
        public async Task<string> GetMoveSubType(string code, string language_code = "en")
        {
            // 없을 경우 예외 처리 필요
            MoveSubType_name moveSubType_Name = await MoveSubTypeService.GetNameEntitiyByBaseCodeAndLanguageCode(int.Parse(code), language_code);
            return moveSubType_Name.Name;
        }
        private CommandDetail GetCommand(string cmd)
        {
            string[] devidedCommands = cmd.Replace("{", "").Replace("}", "").Split(":");
            string[] commandType = new string[] { "S:", "M:", "T:", "U:" };

            string type = "C";
            string command = cmd;
            string data = "";

            if (devidedCommands.Length >= 2)
            {
                type = devidedCommands[0];
                command = devidedCommands[1];

            }
            if (devidedCommands.Length == 3)
            {
                data = devidedCommands[2];
            }
            return new CommandDetail(type, command, data);
        }

        public string GetStateGroupType(int stateGroupCode)
        {
            string type = "S";
            if (stateGroupCode == 80000007)
            {
                type = "M";
            }
            else if (stateGroupCode == 80000016)
            {
                type = "T";
            }
            else if (stateGroupCode == 80000018)
            {
                type = "U";
            }
            return type;
        }


        internal class CommandDetail
        {
            public string Type { get; set; }
            public string Command { get; set; }
            public string Data { get; set; }
            public CommandDetail(string type, string command, string data)
            {
                this.Type = type;
                this.Command = command;
                this.Data = data;
            }
        }
    }

}


