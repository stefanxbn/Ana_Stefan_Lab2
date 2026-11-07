using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ana_Stefan_Lab2.Models;

namespace Ana_Stefan_Lab2.Pages.Borrowings;

public class EditModel : PageModel
{
    private readonly Ana_Stefan_Lab2Context _context;

    public EditModel(Ana_Stefan_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing == null)
        {
            return NotFound();
        }
        Borrowing = borrowing;
        LoadDropdowns();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingExists(Borrowing.ID))
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

    private bool BorrowingExists(int id)
    {
        return _context.Borrowing.Any(e => e.ID == id);
    }

    private void LoadDropdowns()
    {
        var bookList = _context.Book
            .Include(b => b.Author)
            .AsEnumerable()
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + (x.Author != null ? " - " + x.Author.FullName : "")
            })
            .ToList();

        ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");
        ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
    }
}
