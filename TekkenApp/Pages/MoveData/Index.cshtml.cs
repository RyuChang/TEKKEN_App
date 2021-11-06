using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.MoveData
{
    public class IndexModel : PageModel
    {
        private readonly TekkenApp.Data.TekkenDbContext _context;

        public IndexModel(TekkenApp.Data.TekkenDbContext context)
        {
            _context = context;
        }

        public IList<move_data> move_data { get;set; }

        public async Task OnGetAsync()
        {
            move_data = await _context.move_data
                .Include(m => m.Move_CodeNavigation)
                .Include(m => m.counterType_codeNavigation)
                .Include(m => m.guardType_codeNavigation)
                .Include(m => m.hitType_codeNavigation)
                .Include(m => m.moveSubType_codeNavigation)
                .Include(m => m.moveType_codeNavigation)
                .Include(m => m.startType_codeNavigation).ToListAsync();
        }
    }
}
