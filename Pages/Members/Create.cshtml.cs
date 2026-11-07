using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ana_Stefan_Lab2.Models;

namespace Ana_Stefan_Lab2.Pages.Members;

public class CreateModel : PageModel
{
    private readonly Ana_Stefan_Lab2Context _context;

    public CreateModel(Ana_Stefan_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Member Member { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Member.Add(Member);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
