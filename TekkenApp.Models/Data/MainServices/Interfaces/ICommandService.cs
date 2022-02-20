﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface ICommandService : IBaseNameService<Command, Command_name>
    {
        Task<List<KeyMapVM>> GetKeyMaps();
    }
}