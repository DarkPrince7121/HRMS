namespace HRMS.Models.DTOs;

public class SearchRequest
{
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Dictionary<string, string>? Filters { get; set; }
}