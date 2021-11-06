using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TEKKEN_WEB.COMMON.Dapper;

namespace TEKKEN_WEB.COMMON.Command
{
    public class TekkenCommand
    {
        string[] arrayCommand;
        string _language_code;
        string _rawCommand;
        List<CommandInfo> commandList = new List<CommandInfo>();

        private String ResultCommand { get; set; }
        public TekkenCommand()
        {
        }
        public TekkenCommand(string rawCommand, string language_code)
        {
            _rawCommand = rawCommand;
            _language_code = language_code;
            SetCommandList(rawCommand);

            foreach (CommandInfo c in commandList)
            {
                Console.WriteLine("type:{0}, command:{1}", c.Type, c.Command);
                String result = TranseCommand(c, language_code);
                Console.WriteLine(result);
                ResultCommand += result + " ";
            }

            Console.WriteLine(DapperHelper.test());
            Console.WriteLine();

            ;
        }

        public String GetResultcommand()
        {
            return ResultCommand;
        }

        public int CreateForeignCommand(int code)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@move_code", value: code, dbType: DbType.Int32);
            parameters.Add("@name", value: GetResultcommand(), dbType: DbType.String);
            parameters.Add("@language_code", value: _language_code, dbType: DbType.String);


            int result = DapperHelper.Con().Execute("[MoveCommand_CreateMoveCommand_name]", parameters
                        , commandType: CommandType.StoredProcedure);

            return result;
        }

        string TranseCommand(CommandInfo commandInfo, string language_code)
        {
            string result = string.Empty;
            if (commandInfo.Type == "C")
            {
                if (commandInfo.Command.IndexOf("+") > 0)
                {
                    result = TransKey(commandInfo.Command);
                }
                else
                {
                    result = commandInfo.Command;
                }
            }
            else if (commandInfo.Type == "M")
            {
                string[] codes = commandInfo.Command.Split(":");
                result = GetState(int.Parse(codes[0]), language_code).Replace("{SKILL}", GetMove(int.Parse(codes[1]), language_code));
            }
            else if (commandInfo.Type == "T")
            {
                string[] codes = commandInfo.Command.Split(":");
                result = GetState(int.Parse(codes[0]), language_code).Replace("{TEXT}", GetMoveText(int.Parse(codes[1]), language_code));
            }
            else if (commandInfo.Type == "S")
            {
                result = GetState(int.Parse(commandInfo.Command), language_code);
            }
            return result;
        }

        string TransKey(string command)
        {
            string result = string.Empty;


            switch (command)
            {
                case "Q+W":
                    result = "AP";
                    break;
                case "A+S":
                    result = "AK";
                    break;

                case "A+Q":
                    result = "AL";
                    break;

                case "S+W":
                    result = "AR";
                    break;
                default:
                    result = command;
                    break;
            }


            return result;
        }

        private void SetCommandList(string rawCommand)
        {
            arrayCommand = rawCommand.Split('/');

            foreach (string cmd in arrayCommand)
            {
                string type = string.Empty;
                string command = string.Empty;

                if (cmd.Contains("S:"))
                {
                    type = "S";
                    command = cmd.Substring(3, cmd.Length - 4);
                }
                else if (cmd.Contains("M:"))
                {
                    type = "M";
                    command = cmd.Substring(3, cmd.Length - 4);
                }
                else if (cmd.Contains("T:"))
                {
                    type = "T";
                    command = cmd.Substring(3, cmd.Length - 4);
                }
                else
                {
                    command = cmd;
                    type = "C";
                }
                commandList.Add(new CommandInfo(type, command));
            }
        }

        public String GetState(int code, string language_code = "en")
        => DapperHelper.Con().Query<String>("[State_GetStateByCode]",
        new DynamicParameters(new { code = code, language_code = language_code }),
        commandType: CommandType.StoredProcedure).SingleOrDefault();

        public String GetMove(int code, string language_code = "en")
        => DapperHelper.Con().Query<String>("[Move_GetAllMove_NamesByCode]",
        new DynamicParameters(new { code = code, language_code = language_code }),
        commandType: CommandType.StoredProcedure).SingleOrDefault();

        public String GetMoveText(int code, string language_code = "en")
        => DapperHelper.Con().Query<String>("[MoveText_GetMoveText_ByCode]",
        new DynamicParameters(new { code = code, language_code = language_code }),
        commandType: CommandType.StoredProcedure).SingleOrDefault();

        public List<KeyMap> GetKeyMaps()
        => DapperHelper.Con().Query<KeyMap>("[COMMAND_GetAllKeys]",
        commandType: CommandType.StoredProcedure).ToList<KeyMap>();


        private class CommandInfo
        {
            public string Type { get; set; }
            public string Command { get; set; }
            public CommandInfo(string type, string command)
            {
                Type = type;
                Command = command;
            }
        }

        public class KeyMap
        {
            public string Key { get; set; }
            public string Code { get; set; }

            public KeyMap(string key, string code)
            {
                Key = key;
                Code = code;
            }

        }
    }

}
