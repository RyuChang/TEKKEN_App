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
    public class IndexModel : PageModel
    {
        private readonly TekkenApp.Data.TekkenDbContext _context;

        public IndexModel(TekkenApp.Data.TekkenDbContext context)
        {
            _context = context;
        }

        public IList<hitType> hitType { get;set; }

        public async Task OnGetAsync()
        {
            hitType = await _context.hitType.ToListAsync();
        }
    }
}
