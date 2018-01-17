namespace vega.Extrnsions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool isSortAscending { get; set; }
        int Page { get; set; }
        byte PageSize { get; set; }
    }
}