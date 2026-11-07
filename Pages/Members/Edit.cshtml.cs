using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ana_Stefan_Lab2.Models;

namespace Ana_Stefan_Lab2.Pages.Members;

public class EditModel : PageModel
{
    private readonly Ana_Stefan_Lab2Context _context;

    public EditModel(Ana_Stefan_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Member Member { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var member = await _context.Member.FirstOrDefaultAsync(m => m.ID == id);
        if (member == null)
        {
            return NotFound();
        }
        Member = member;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Member).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MemberExists(Member.ID))
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

    private bool MemberExists(int id)
    {
        return _context.Member.Any(e => e.ID == id);
    }
}
