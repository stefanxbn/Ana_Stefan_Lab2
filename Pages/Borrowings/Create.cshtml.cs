using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ana_Stefan_Lab2.Models;

namespace Ana_Stefan_Lab2.Pages.Borrowings;

public class CreateModel : PageModel
{
    private readonly Ana_Stefan_Lab2Context _context;

    public CreateModel(Ana_Stefan_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        LoadDropdowns();
        return Page();
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
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
