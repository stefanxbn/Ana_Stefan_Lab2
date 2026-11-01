using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ana_Stefan_Lab2.Models;
using Ana_Stefan_Lab2.ViewModels;

namespace Ana_Stefan_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Ana_Stefan_Lab2Context _context;

    public IndexModel(Ana_Stefan_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;
    public CategoryIndexData CategoryData { get; set; } = default!;
    public int CategoryID { get; set; }
    public bool CategorySelected { get; set; }

    public async Task OnGetAsync(int? id)
    {
        CategoryData = new CategoryIndexData();
        CategoryData.Categories = await _context.Category
            .Include(c => c.BookCategories!)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;
            Category? category = CategoryData.Categories
                .SingleOrDefault(c => c.ID == id.Value);
            if (category != null)
            {
                CategorySelected = true;
                CategoryData.Books = (category.BookCategories ?? new List<BookCategory>())
                    .Select(bc => bc.Book)
                    .OrderBy(b => b.Title)
                    .ToList();
            }
        }
    }
}
