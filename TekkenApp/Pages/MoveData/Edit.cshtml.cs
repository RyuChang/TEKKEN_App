using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.MoveData
{
    public class EditModel : PageModel
    {
        private readonly TekkenApp.Data.TekkenDbContext _context;

        public EditModel(TekkenApp.Data.TekkenDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["Move_Code"] = new SelectList(_context.Move, "code", "description");
           ViewData["counterType_code"] = new SelectList(_context.hitType, "code", "description");
           ViewData["guardType_code"] = new SelectList(_context.hitType, "code", "description");
           ViewData["hitType_code"] = new SelectList(_context.hitType, "code", "description");
           ViewData["moveSubType_code"] = new SelectList(_context.moveSubType, "code", "description");
           ViewData["moveType_code"] = new SelectList(_context.moveType, "code", "description");
           ViewData["startType_code"] = new SelectList(_context.hitType, "code", "description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(move_data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!move_dataExists(move_data.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool move_dataExists(int id)
        {
            return _context.move_data.Any(e => e.id == id);
        }
    }
}
