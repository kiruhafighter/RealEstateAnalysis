using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateAnalysis.Client.Enums;
using RealEstateAnalysis.Client.ViewModels;

namespace RealEstateAnalysis.Client.Pages;

public class AveragePrices : PageModel
{
    private readonly IClient _client;

    public AveragePrices(IClient client)
    {
        _client = client;
    }
    
    [BindProperty]
    public GetAveragePricesSampleRequest Request { get; set; }
    
    public List<AveragePriceForMonth>? AveragePricesList { get; set; }
    
    public SelectList PropertyTypes { get; set; }
    public SelectList Years { get; set; }
    public SelectList Months { get; set; }

    public void OnGet()
    {
        PropertyTypes = new SelectList(Enum.GetValues(typeof(PropertyType)).Cast<PropertyType>());
        
        var currentYear = DateTime.Now.Year;
        Years = new SelectList(Enumerable.Range(2000, currentYear - 1999));
        
        var months = Enumerable.Range(1, 12)
            .Select(m => new SelectListItem 
            {
                Value = m.ToString(), 
                Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
            })
            .ToList();
        Months = new SelectList(months, "Value", "Text");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            OnGet(); // Repopulate dropdowns on postback
            return Page();
        }
        
        var averagePriceForMonths = await _client.GetAveragePricesForPeriodAsync(
            Request.Name,
            Request.Address,
            Request.County,
            Request.Country,
            Request.Locality,
            (int?)Request.PropertyTypeId,
            Request.NumberOfRooms,
            Request.NumberOfFloors,
            Request.MinYearBuilt,
            Request.MaxYearBuilt,
            Request.MinPlotArea,
            Request.MaxPlotArea,
            Request.MinFloorArea,
            Request.MaxFloorArea,
            Request.MinPrice,
            Request.MaxPrice,
            Request.StartYear,
            Request.StartMonth,
            Request.EndYear,
            Request.EndMonth);
        
        AveragePricesList = averagePriceForMonths.ToList();

        return Page();
    }
}