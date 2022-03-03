using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class CommandService : BaseNameService<Command, Command_name>, ICommandService
    {
        public static List<KeyMapVM> KeyMap { get; set; }
        List<CommandInfo> commandList = new List<CommandInfo>();

        public CommandService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.Command, tekkenDbContext.Command_name)
        {
            MainTable = TableName.Command.ToString();
            NameTable = TableName.Command_name.ToString();
            SetKeyMaps();
        }

        /* public void SetCommandList(string rawCommand)
         {
             string[] arrayCommand = rawCommand.Split('/');
             string ResultCommand = string.Empty;
             foreach (string cmd in arrayCommand)
             {
                 string type = string.Empty;
                 string command = string.Empty;

                 command = GetCommand(cmd, out type);
                 CommandInfo commandInfo = new CommandInfo(type, command);
                 String result = TranseCommand(commandInfo, language_code);

                 ResultCommand += result + " ";
             }

             *//*foreach (CommandInfo c in commandList)
             {
                 Console.WriteLine("type:{0}, command:{1}", c.Type, c.Command);
                 Console.WriteLine(result);
             }*//*
         }*/

        /*string TranseCommand(CommandInfo commandInfo, string language_code)
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
        }*/

        private static string GetCommand(string cmd, out string type)
        {
            string[] commandType = new string[] { "S:", "M:", "T:", "C:" };
            type = "C";

            foreach (string s in commandType)
            {
                if (cmd.Contains(s))
                {
                    type = s;
                }
            }

            return cmd.Substring(3, cmd.Length - 4);
        }

        private async void SetKeyMaps()
        {
            KeyMap = await GetKeyMaps();
        }

        public async Task<List<KeyMapVM>> GetKeyMaps()
        {
            if (KeyMap is null)
            {
                return await _dataDbSet.Select(p => new KeyMapVM
                {
                    Code = p.CommandCode,
                    Key = p.key
                }).ToListAsync();
            }
            else
            {
                return KeyMap;
            }
        }

        internal class CommandInfo
        {
            public string Type { get; set; }
            public string Command { get; set; }
            public CommandInfo(string type, string command)
            {
                Type = type;
                Command = command;
            }
        }
    }
}


