using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ana_Stefan_Lab2.Models;
using Ana_Stefan_Lab2.ViewModels;

namespace Ana_Stefan_Lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly Ana_Stefan_Lab2Context _context;

    public IndexModel(Ana_Stefan_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;
    public PublisherIndexData PublisherData { get; set; } = default!;
    public int PublisherID { get; set; }
    public int BookID { get; set; }
    public bool PublisherSelected { get; set; }

    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();
        PublisherData.Publishers = await _context.Publisher
            .Include(i => i.Books!)
                .ThenInclude(c => c.Author)
            .OrderBy(i => i.PublisherName)
            .ToListAsync();

        if (id != null)
        {
            PublisherID = id.Value;
            Publisher? publisher = PublisherData.Publishers
                .SingleOrDefault(i => i.ID == id.Value);
            if (publisher != null)
            {
                PublisherSelected = true;
                PublisherData.Books = publisher.Books ?? new List<Book>();
            }
        }
    }
}
