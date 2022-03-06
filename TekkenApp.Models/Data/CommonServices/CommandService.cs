using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class CommandService : BaseNameService<Command, Command_name>, ICommandService
    {
        //List<CommandInfo> commandList = new List<CommandInfo>();
        IStateService StateService { get; set; }
        IMoveService MoveService { get; set; }
        IMoveTextService MoveTextService { get; set; }

        ICommanderMapperService CommanderMapperService { get; set; }
        public int Timer { get; set; }

        public string RawCommand { get; set; }
        public string DisplayCommand { get; set; }
        public List<string> resultKey { get; set; }
        public List<string> clickedKey { get; set; }


        public CommandService(TekkenDbContext tekkenDbContext, ICommanderMapperService _commanderMapperService, IStateService _stateService, IMoveService _moveService, IMoveTextService _moveTextService) : base(tekkenDbContext, tekkenDbContext.Command, tekkenDbContext.Command_name)
        {
            MainTable = TableName.Command.ToString();
            NameTable = TableName.Command_name.ToString();

            this.StateService = _stateService;
            this.MoveService = _moveService;
            this.MoveTextService = _moveTextService;

            this.CommanderMapperService = _commanderMapperService;
        }

        #region Key 입력 이벤트 처리
        public void InitCommand(string rawCommand)
        {
            this.RawCommand = rawCommand;
            clickedKey = new List<string>();
            resultKey = new List<string>();
        }
        public void ClearCommand()
        {
            clickedKey.Clear();
            resultKey.Clear();
            Timer = 0;
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

        public void RemoveKey(string key)
        {
            bool isExist = this.resultKey.Contains(key);
            if (isExist)
            {
                resultKey.Remove(key);
            }

            if (resultKey.Count == 0)
            {
                AddCommand(key);
            }
        }
        #endregion

        public void AddCommand(string key)
        {
            string result = RawCommand;
            string formedKey = "";

            if (key == "Backspace")
            {
                int lastIndex = RawCommand.LastIndexOf("/");
                if (lastIndex < 0) lastIndex = 0;
                SetCommand(RawCommand.Substring(0, lastIndex));
                return;
            }
            else if (clickedKey.Count == 1)
            {
                formedKey = key.ToUpper();
            }
            else if (clickedKey.Count >= 2)
            {
                clickedKey.Sort();
                formedKey = String.Join("+", clickedKey.ToArray()).ToUpper();
            }
            if (Timer > 2)
            {
                formedKey = 'L' + formedKey.ToUpper();
            }


            string mapppedKey = CommanderMapperService.MapKey(formedKey);
            if (!string.IsNullOrEmpty(mapppedKey))
            {
                result += '/' + mapppedKey;
            }

            SetCommand(result);

        }

        public async Task SetCommand(string result)
        {
            if (result.Length > 0 && result[0] == '/')
            {
                result = result.Substring(1, result.Length - 1);
            }

            RawCommand = result;
            DisplayCommand = await TransCommand(RawCommand, "en");
            ClearCommand();
            //StateHasChanged();
        }

        public string GetRawCommand()
        {
            return RawCommand;
        }

        public string GetDisplayCommand()
        {
            return DisplayCommand;
        }


        public async Task AddState(string stateGroupType, int stateCode, int dataCode = 0)
        {
            var result = $"{RawCommand}/{{{stateGroupType}:{stateCode}{(dataCode == 0 ? "" : $":{dataCode}")}}}";
            await SetCommand(result);
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
            else if (commandInfo.Type == "M")
            {
                string moveText = await GetMoveText(commandInfo.Data, language_code);
                string stateName = CommanderMapperService.MapState(commandInfo.Command, language_code);
                result = stateName.Replace("{MOVE}", moveText);
            }
            else if (commandInfo.Type == "T")
            {
                string[] codes = commandInfo.Command.Split(":");
                //result = await GetMoveText(int.Parse(codes[0]), language_code).Result.Replace("{TEXT}", await GetMoveText(int.Parse(codes[1]), language_code));
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

        private CommandDetail GetCommand(string cmd)
        {
            string[] devidedCommands = cmd.Replace("{", "").Replace("}", "").Split(":");
            string[] commandType = new string[] { "S:", "M:", "T:" };

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
            else if (stateGroupCode == 80000015)
            {
                type = "T";
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


