using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace TekkenApp.Pages.HitType
{
    public class DetailsModel : PageModel
    {
        private readonly TekkenApp.Data.TekkenDbContext _context;

        public DetailsModel(TekkenApp.Data.TekkenDbContext context)
        {
            _context = context;
        }

        public hitType hitType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            hitType = await _context.hitType.FirstOrDefaultAsync(m => m.id == id);

            if (hitType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
