using App.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Extensions;

public static class PageExtensions
{
    // yazdım ama kullanmadım. çünkü handler sınıfımda PageModel nesneme de ihtiyacım oluyor!
    public static async Task<IQueryable<T>> GetPage<T>(this IQueryable<T> query, int currentPage, int pageCapability)
    where T : class
    {
        if (currentPage == 0 || pageCapability == 0)
            return query;

        var pageInfo = new PageModel(
                CurrentPage: currentPage,
                PageCapability: pageCapability,
                TotalRowCount: await query.CountAsync()
            );

        return query.Skip(pageInfo.Skipped).Take(pageInfo.PageCapability);
    }
}
