namespace TradeWise.Helpers
{
    public class QueryObject
    {
        public string? StockName { get; set; } = string.Empty;
        public string? CompanyName { get; set; } = string.Empty;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
