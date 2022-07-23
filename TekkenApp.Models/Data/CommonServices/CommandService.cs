using System;
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
        public string RawCommand { get; set; }


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

        /// <summary>
        /// RawCommand 구분자 제거
        /// </summary>
        public async Task SetCommand()
        {
            if (RawCommand.Length > 0 && RawCommand[0] == '/')
            {
                RawCommand = RawCommand.Substring(1);
            }
        }

        /// <summary>
        /// State를 입력받은 형식에 맞게 변환하여 리턴
        /// </summary>
        /// <param name="stateGroupType"></param>
        /// <param name="stateCode"></param>
        /// <param name="dataCode"></param>
        /// <returns>State추가 된 RawCommand</returns>
        public string AddState(string stateGroupType, int stateCode, int dataCode = 0)
        {
            return $"{RawCommand}/{{{stateGroupType}:{stateCode}{(dataCode == 0 ? "" : $":{dataCode}")}}}";
        }

        /// <summary>
        /// 언어에 맞게 변환된 TAG 커맨드 리턴
        /// </summary>
        /// <param name="rawCommand"></param>
        /// <param name="language_code"></param>
        /// <returns>이미지로 변환된 TAG 커맨드</returns>
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

        /// <summary>
        /// StateGroup 코드를 이용하여 StateGroup타입 리턴
        /// </summary>
        /// <param name="stateGroupCode"></param>
        /// <returns>StateGroup타입 </returns>
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


        internal String TranseCommandToImage(String command)
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

        /// <summary>
        /// moveCode와 언어를 입력 값으로 받아 MoveText출력
        /// 언어 기본값은 영어
        /// </summary>
        /// <param name="code"></param>
        /// <param name="language_code"></param>
        /// <returns>MoveText 문자열 출력</returns>
        internal async Task<string> GetMoveText(string code, string language_code = "en")
        {
            // 없을 경우 예외 처리 필요
            MoveText_name moveText_Name = await MoveTextService.GetNameEntitiyByBaseCodeAndLanguageCode(int.Parse(code), language_code);
            return moveText_Name.Name;
        }

        /// <summary>
        /// moveCode와 언어를 입력 값으로 받아 Move명 출력
        /// 언어 기본값은 영어
        /// </summary>
        /// <param name="code"></param>
        /// <param name="language_code"></param>
        /// <returns>Move 문자열 출력</returns>
        internal async Task<string> GetMove(string code, string language_code = "en")
        {
            // 없을 경우 예외 처리 필요
            Move_name move_Name = await MoveService.GetNameEntitiyByBaseCodeAndLanguageCode(int.Parse(code), language_code);
            return move_Name.Name;
        }

        /// <summary>
        /// moveCode와 언어를 입력 값으로 받아 MoveSubType명 출력
        /// 언어 기본값은 영어
        /// </summary>
        /// <param name="code"></param>
        /// <param name="language_code"></param>
        /// <returns>Move 문자열 출력</returns>
        internal async Task<string> GetMoveSubType(string code, string language_code = "en")
        {
            // 없을 경우 예외 처리 필요
            MoveSubType_name moveSubType_Name = await MoveSubTypeService.GetNameEntitiyByBaseCodeAndLanguageCode(int.Parse(code), language_code);
            return moveSubType_Name.Name;
        }

        /// <summary>
        /// state와 move코드를 받아 CommandDetail객체로 변환된 값을 출력
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>CommandDetail 객체</returns>
        internal CommandDetail GetCommand(string cmd)
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


