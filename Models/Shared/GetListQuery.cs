using System.ComponentModel.DataAnnotations;

namespace Models.Shared
{
    public class GetListQuery
    {
        public int Offset { get; set; }

        [Required]
        public int Limit { get; set; }
    }
}
