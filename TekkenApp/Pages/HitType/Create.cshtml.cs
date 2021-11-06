using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.HitType
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
            return Page();
        }

        [BindProperty]
        public hitType hitType { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.hitType.Add(hitType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
