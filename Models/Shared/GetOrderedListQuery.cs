using Models.Enums;

namespace Models.Shared
{
    public class GetOrderedListQuery : GetListQuery
    {
        public string OrderField { get; set; }

        public OrderDirections? OrderDirection { get; set; }
    }
}
