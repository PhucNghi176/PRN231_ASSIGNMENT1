using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.SilverJewelryPages;
public class IndexModel : PageModel
{
    private readonly string _baseUrl = "https://localhost:7292/api/Silver";
    public IList<SilverJewelry> SilverJewelry { get; set; } = default!;
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 7; // Adjust page size as needed

    public async Task OnGetAsync(int? pageNumber)
    {
        CurrentPage = pageNumber ?? 1;
        using var httpClient = new HttpClient();
        var pagedUrl = $"{_baseUrl}?page={CurrentPage}&pageSize={PageSize}"; // Adjust query params as per API

        var response = await httpClient.GetAsync(pagedUrl);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // Assuming the API response includes total count and items (adjust accordingly)
            var pagedResponse = JsonSerializer.Deserialize<PagedResult<SilverJewelry>>(content, options);
            SilverJewelry = pagedResponse.Items;
            TotalPages = (int)Math.Ceiling((double)pagedResponse.TotalCount / PageSize);
        }
    }
}