using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class CommandService : BaseService<Command, Command_name>, ICommandService
    {

        public CommandService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.Command, tekkenDbContext.Command_name)
        {
            MainTable = TableName.Command.ToString();
            NameTable = TableName.Command_name.ToString();
            //App = AppType.HitType;
        }

        public virtual async Task<List<KeyMapVM>> GetKeyMaps()
        {
            return await _dataDbSet.Select(p => new KeyMapVM
            {
                Code = p.CommandCode,
                Key = p.key
            }).ToListAsync();
        }

    }
}


