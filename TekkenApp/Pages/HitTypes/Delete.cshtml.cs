using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TekkenApp.Pages.HitTypes
{
    public class DeleteModel : PageModel
    {
        private readonly TekkenApp.Data.TekkenDbContext _context;

        public DeleteModel(TekkenApp.Data.TekkenDbContext context)
        {
            _context = context;
        }

       // [BindProperty]
       // public Models.HitType hitType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          //  hitType = await _context.hitType.FirstOrDefaultAsync(m => m.id == id);

           // if (hitType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          //  hitType = await _context.hitType.FindAsync(id);

          //  if (hitType != null)
            {
           //     _context.hitType.Remove(hitType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
