using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public class CommandService<TDataEntity, TNameEntity> : BaseService<Command, Command_name>

    {

        public CommandService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.Command, tekkenDbContext.Command_name)
        {
            MainTable = TableName.Command.ToString();
            NameTable = TableName.Command_name.ToString();
            //App = AppType.HitType;
        }

        public virtual async Task<List<CommandVM>> GetCommands()
        {
            return await _dataDbSet.Select(p => new CommandVM
            {
                Command = p.Command,
                Code = p.Code
            }).ToListAsync();
        }

    }
}


