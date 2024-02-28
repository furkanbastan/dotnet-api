using App.Domain.Exceptions;

namespace App.Domain.Models;

public class PageModel
{
    public PageModel(int TotalRowCount) : this(1, 1, TotalRowCount)
    {

    }
    public PageModel(int PageCapability, int TotalRowCount) : this(1, PageCapability, TotalRowCount)
    {

    }
    public PageModel(int CurrentPage, int PageCapability, int TotalRowCount)
    {
        if (CurrentPage < 1)
            throw new ValidationExceptions("CurrentPage Error!");

        if (PageCapability < 1)
            throw new ValidationExceptions("PageCapability Error!");

        this.CurrentPage = CurrentPage;
        this.PageCapability = PageCapability;
        this.TotalRowCount = TotalRowCount;

        if (CurrentPage > TotalPageCount)
            throw new ValidationExceptions("CurrentPage>TotalPageCount Error!");
    }
    public int CurrentPage { get; set; }
    public int PageCapability { get; set; }
    public int TotalRowCount { get; set; }
    public int TotalPageCount => (int)Math.Ceiling((double)TotalRowCount / PageCapability);
    public int Skipped => (CurrentPage - 1) * PageCapability;
    public bool HasBack => CurrentPage != 1;
    public bool HasNext => CurrentPage != TotalPageCount;
}
