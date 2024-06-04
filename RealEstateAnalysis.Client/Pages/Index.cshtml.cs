using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateAnalysis.Client.Enums;
using RealEstateAnalysis.Client.ViewModels;

namespace RealEstateAnalysis.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IClient _client;

        public IndexModel(IClient client)
        {
            _client = client;
        }
        
        [BindProperty]
        public new GetFilteredPropertiesRequest Request { get; set; }
        
        public ICollection<PropertyListedDto> Properties { get; set; } = new List<PropertyListedDto>();
        
        public string? ErrorMessage { get; set; }
        
        public SelectList PropertyTypes { get; set; }
        
        [BindProperty]
        public int PageNumber { get; set; } = 1;
        
        [BindProperty]
        public int PageSize { get; set; } = 10;
        
        public int TotalPages { get; set; }
        
        public int TotalCount { get; set; }
        
        public bool FirstRequestMade { get; set; } = false;

        public async Task<IActionResult> OnGetAsync()
        {
            if (FirstRequestMade)
            {
                await LoadFilteredProperties();
            }
            
            PopulateDropdowns();

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await LoadFilteredProperties();
            
            FirstRequestMade = true;
                        
            PopulateDropdowns();

            return Page();
        }

        private async Task LoadFilteredProperties()
        {
            try
            {
                var result = await _client.GetPropertiesFilteredAsync(PageNumber, PageSize,
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
                    Request.MaxPrice);
                
                Properties = result.Data;
                TotalCount = result.TotalCount;
                
                TotalPages = (int)Math.Ceiling(result.TotalCount / (double)PageSize);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
        
        private void PopulateDropdowns()
        {
            var propertyTypes = Enum.GetValues(typeof(PropertyType))
                .Cast<PropertyType>()
                .Select(pt => new SelectListItem
                {
                    Value = ((int)pt).ToString(),
                    Text = pt.ToString()
                })
                .ToList();

            // Add the "Any" option with a null value at the beginning of the list
            propertyTypes.Insert(0, new SelectListItem { Value = null, Text = "Any" });

            PropertyTypes = new SelectList(propertyTypes, "Value", "Text");
        }
        
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber * PageSize < TotalCount;
    }
}
