using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateAnalysis.Client.Enums;

namespace RealEstateAnalysis.Client.Pages;

public class AddPropertyModel : PageModel
{
    private readonly IClient _client;

    public AddPropertyModel(IClient client)
    {
        _client = client;
    }
    
    [BindProperty]
    public AddPropertyDto AddPropertyDto { get; set; } = new();
    
    [BindProperty]
    public List<IFormFile> UploadImages { get; set; } = new();
    
    public string? ErrorMessage { get; set; }
    
    public SelectList PropertyTypes { get; set; }
    
    public SelectList PropertyStatuses { get; set; }

    public IActionResult OnGet()
    {
        PopulateDropdowns();
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(string? cancel)
    {
        if (!string.IsNullOrEmpty(cancel))
        {
            return RedirectToPage("/Index");
        }
        
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Invalid data";
            PopulateDropdowns();
            return Page();
        }

        try
        {
            var images = new List<AddImageDto>();
            foreach (var file in UploadImages)
            {
                var fileName = Path.GetFileName(file.FileName);
                images.Add(new AddImageDto { ImagePath = $"/images/{fileName}" });
            }

            AddPropertyDto.Images = images;

            await _client.AddPropertyAsync(AddPropertyDto);
            return RedirectToPage("/Index");
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            PopulateDropdowns();
            return Page();
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

        PropertyTypes = new SelectList(propertyTypes, "Value", "Text");
        
        var propertyStatuses = Enum.GetValues(typeof(PropertyStatus))
            .Cast<PropertyStatus>()
            .Select(ps => new SelectListItem
            {
                Value = ((int)ps).ToString(),
                Text = ps.ToString()
            })
            .ToList();

        PropertyStatuses = new SelectList(propertyStatuses, "Value", "Text");
    }
}