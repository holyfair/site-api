using System.ComponentModel.DataAnnotations;

namespace Models.Sources
{
    public class Author
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FathersName { get; set; }
    }
}
