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
    public class DetailsModel : PageModel
    {
        private readonly TekkenApp.Data.TekkenDbContext _context;

        public DetailsModel(TekkenApp.Data.TekkenDbContext context)
        {
            _context = context;
        }

        public move_data move_data { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            move_data = await _context.move_data
                .Include(m => m.Move_CodeNavigation)
                .Include(m => m.counterType_codeNavigation)
                .Include(m => m.guardType_codeNavigation)
                .Include(m => m.hitType_codeNavigation)
                .Include(m => m.moveSubType_codeNavigation)
                .Include(m => m.moveType_codeNavigation)
                .Include(m => m.startType_codeNavigation).FirstOrDefaultAsync(m => m.id == id);

            if (move_data == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
