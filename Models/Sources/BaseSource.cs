using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models.Sources
{
    public class BaseSource : IBaseSource
    {
        [Required]
        public string WorkName { get; set; }

        public List<Author> Authors { get; set; }

        public virtual SourceTypes Type { get; set; }

        public string ParseAuthor()
        {
            if (Authors is null || Authors.Count == 0)
            {
                return string.Empty;
            }
            var author = Authors.First();

            var firstName = !string.IsNullOrEmpty(author.FirstName) ? $" {author.FirstName.Substring(0, 1)}." : string.Empty;
            var fathersName = !string.IsNullOrEmpty(author.FathersName) ? $" {author.FathersName.Substring(0, 1)}. " : string.Empty;
            var lastName = !string.IsNullOrEmpty(author.LastName) ? $"{author.LastName}" : string.Empty;

            return $"{lastName}{firstName}{fathersName}";
        }

        public string ParseAllAuthors()
        {
            if(Authors is null || Authors.Count == 0)
            {
                return string.Empty;
            }

            if(Authors.Count == 1)
            {
                var author = Authors.First();
                var firstName = !string.IsNullOrEmpty(author.FirstName) ? $" {author.FirstName}" : string.Empty;
                var fathersName = !string.IsNullOrEmpty(author.FathersName) ? $" {author.FathersName}" : string.Empty;
                var lastName = !string.IsNullOrEmpty(author.LastName) ? $"{author.LastName}" : string.Empty;

                return $" / {lastName}{firstName}{fathersName}";
            }

            var result = " / ";
           
            for(int i = 0; i < Authors.Count - 1; i++)
            {
                var author = Authors[i];
                result += $"{ConvertAuthorToString(author)}, ";
            }

            var lastAuthor = Authors.Last();
            result += $"{ConvertAuthorToString(lastAuthor)}";

            return result;
        }

        private string ConvertAuthorToString(Author author)
        {
            var firstName = !string.IsNullOrEmpty(author.FirstName) ? $"{author.FirstName.Substring(0, 1)}. " : string.Empty;
            var fathersName = !string.IsNullOrEmpty(author.FathersName) ? $"{author.FathersName.Substring(0, 1)}. " : string.Empty;
            var lastName = !string.IsNullOrEmpty(author.LastName) ? author.LastName : string.Empty;

            return $"{firstName}{fathersName}{lastName}";
        }
    }
}
