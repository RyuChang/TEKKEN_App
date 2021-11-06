using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.MoveData
{
    public class CreateModel : PageModel
    {
        private readonly TekkenApp.Data.TekkenDbContext _context;

        public CreateModel(TekkenApp.Data.TekkenDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Move_Code"] = new SelectList(_context.Move, "code", "description");
        ViewData["counterType_code"] = new SelectList(_context.hitType, "code", "description");
        ViewData["guardType_code"] = new SelectList(_context.hitType, "code", "description");
        ViewData["hitType_code"] = new SelectList(_context.hitType, "code", "description");
        ViewData["moveSubType_code"] = new SelectList(_context.moveSubType, "code", "description");
        ViewData["moveType_code"] = new SelectList(_context.moveType, "code", "description");
        ViewData["startType_code"] = new SelectList(_context.hitType, "code", "description");
            return Page();
        }

        [BindProperty]
        public move_data move_data { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.move_data.Add(move_data);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
