using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.HitTypes
{
    public class EditModel : PageModel
    {
        private readonly TekkenDbContext _context;

        public EditModel(TekkenApp.Data.TekkenDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public HitType hitType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            hitType = await _context.hitType.FirstOrDefaultAsync(m => m.Id == id);

            if (hitType == null)
            {
                return NotFound();
            }
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

            _context.Attach(hitType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!hitTypeExists(hitType.Id))
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

        private bool hitTypeExists(int id)
        {
            return _context.hitType.Any(e => e.Id == id);
        }
    }
}
