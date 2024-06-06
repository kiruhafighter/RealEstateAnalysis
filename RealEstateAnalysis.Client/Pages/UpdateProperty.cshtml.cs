using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class UpdatePropertyModel : PageModel
{
    private readonly IClient _client;

    public UpdatePropertyModel(IClient client)
    {
        _client = client;
    }
    
    [BindProperty]
    public UpdatePropertyDto UpdatePropertyDto { get; set; } = new();
    
    [BindProperty]
    public IFormFile? UploadImage { get; set; }
    
    public IList<ImageListedDto>? PropertyImages { get; set; }
    
    public string? ErrorMessage { get; set; }
    
    public async Task<IActionResult> OnGet(Guid id)
    {
        try
        {
            var property = await _client.GetPropertyDetailsAsync(id);

            UpdatePropertyDto = new UpdatePropertyDto
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Address = property.Address,
                Country = property.Country,
                County = property.County,
                Locality = property.Locality,
                Postcode = property.Postcode,
                PropertyTypeId = property.PropertyTypeId,
                NumberOfRooms = property.NumberOfRooms,
                NumberOfFloors = property.NumberOfFloors,
                YearBuilt = property.YearBuilt,
                PlotArea = property.PlotArea,
                FloorArea = property.FloorArea,
                Price = property.Price,
                PropertyStatusId = property.PropertyStatusId
            };

            PropertyImages = property.Images.ToList();
            
            return Page();
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return RedirectToPage("/Error");
        }
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Invalid data";
            return Page();
        }

        try
        {
            await _client.UpdatePropertyAsync(UpdatePropertyDto);

            if (UploadImage != null)
            {
                var imageName = Path.GetFileName(UploadImage.FileName);

                var addImageDto = new AddImageDto
                {
                    ImagePath = $"/images/{imageName}"
                };
                
                await _client.AddImageForPropertyAsync(UpdatePropertyDto.Id, addImageDto);
            }
            
            return RedirectToPage("/PropertyDetails", new { id = UpdatePropertyDto.Id });
        }
        catch(ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }
    
    public async Task<IActionResult> OnPostDeleteImageAsync(Guid propertyId, int imageId)
    {
        try
        {
            await _client.DeleteImageForPropertyAsync(propertyId, imageId);
            return RedirectToPage();
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage("/PropertyDetails", new { id = UpdatePropertyDto.Id });
    }
}