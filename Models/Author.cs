using System.ComponentModel.DataAnnotations;

namespace Ana_Stefan_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => LastName + " " + FirstName;

        public ICollection<Book>? Books { get; set; }
    }
}
