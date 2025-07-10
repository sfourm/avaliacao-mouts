namespace Ambev.DeveloperEvaluation.Packages.Application.Models;

public class PagingResult<T>
{
    public IEnumerable<T> Data { get; set; } = [];
    public int? Hits { get; set; }
}