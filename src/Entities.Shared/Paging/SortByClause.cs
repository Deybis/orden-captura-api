namespace Entities.Shared.Paging
{
    public class SortByClause
    {
        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
        public override string ToString()
        {
            return $"{SortBy} {(this.SortOrder == SortOrder.Ascending ? "asc" : "desc")}";
        }
    }
    public enum SortOrder
    {
        Ascending = 1,
        Descending = 2
    }
}
