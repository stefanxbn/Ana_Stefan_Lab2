using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ana_Stefan_Lab2.Models;

namespace Ana_Stefan_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Ana_Stefan_Lab2Context _context;

    public IndexModel(Ana_Stefan_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;
    public BookData BookD { get; set; } = default!;
    public int BookID { get; set; }
    public int CategoryID { get; set; }
    public string TitleSort { get; set; } = string.Empty;
    public string AuthorSort { get; set; } = string.Empty;
    public string CurrentFilter { get; set; } = string.Empty;

    public async Task OnGetAsync(int? id, int? categoryID, string? sortOrder, string? searchString)
    {
        BookD = new BookData();

        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";
        CurrentFilter = searchString ?? string.Empty;

        BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories!)
                .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        if (!String.IsNullOrEmpty(searchString))
        {
            string term = searchString;
            BookD.Books = BookD.Books.Where(s =>
                s.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                || (s.Author != null
                    && (s.Author.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase)
                        || s.Author.LastName.Contains(term, StringComparison.OrdinalIgnoreCase))));
        }

        if (id != null)
        {
            BookID = id.Value;
            Book? book = BookD.Books.SingleOrDefault(i => i.ID == id.Value);
            if (book != null)
            {
                BookD.Categories = book.BookCategories!.Select(s => s.Category);
            }
        }

        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                break;
            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Author?.FullName);
                break;
            case "author":
                BookD.Books = BookD.Books.OrderBy(s => s.Author?.FullName);
                break;
            default:
                BookD.Books = BookD.Books.OrderBy(s => s.Title);
                break;
        }
    }
}
