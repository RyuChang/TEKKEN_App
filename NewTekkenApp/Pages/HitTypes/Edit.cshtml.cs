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
        public HitType HitType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HitType = await _context.HitType.FirstOrDefaultAsync(m => m.Id == id);

            if (HitType == null)
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

            _context.Attach(HitType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HitTypeExists(HitType.Id))
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

        private bool HitTypeExists(int id)
        {
            return _context.HitType.Any(e => e.Id == id);
        }
    }
}
