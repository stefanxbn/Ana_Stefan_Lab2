using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ana_Stefan_Lab2.Models;

namespace Ana_Stefan_Lab2.Pages.Borrowings;

public class DetailsModel : PageModel
{
    private readonly Ana_Stefan_Lab2Context _context;

    public DetailsModel(Ana_Stefan_Lab2Context context)
    {
        _context = context;
    }

    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b!.Author)
            .Include(b => b.Book)
                .ThenInclude(b => b!.Publisher)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing == null)
        {
            return NotFound();
        }
        Borrowing = borrowing;
        return Page();
    }
}
