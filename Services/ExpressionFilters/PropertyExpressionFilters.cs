using System.Linq.Expressions;
using Domain.Entities;

namespace Services.ExpressionFilters
{
    internal static class PropertyExpressionFilters
    {
        internal static Expression<Func<Property, bool>> FilterByAgentId(Guid agentId)
        {
            return pr =>
                pr.AgentId.Equals(agentId);
        }
        
        internal static Expression<Func<Property, bool>> FilterByName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _ => true;
            }
            
            return pr =>
                pr.Name.Contains(name.Trim());
        }
        
        internal static Expression<Func<Property, bool>> FilterByAddress(string? address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return _ => true;
            }
            
            return pr =>
                pr.Address.Contains(address.Trim());
        }
        
        internal static Expression<Func<Property, bool>> FilterByCounty(string? county)
        {
            if (string.IsNullOrEmpty(county))
            {
                return _ => true;
            }
            
            return pr =>
                pr.County.Contains(county.Trim());
        }
        
        internal static Expression<Func<Property, bool>> FilterByCountry(string? country)
        {
            if (string.IsNullOrEmpty(country))
            {
                return _ => true;
            }
            
            return pr =>
                pr.Country.Contains(country.Trim());
        }
        
        internal static Expression<Func<Property, bool>> FilterByLocality(string? locality)
        {
            if (string.IsNullOrEmpty(locality))
            {
                return _ => true;
            }
            
            return pr =>
                pr.Locality.Contains(locality.Trim());
        }
        
        internal static Expression<Func<Property, bool>> FilterByPropertyTypeId(int? propertyTypeId)
        {
            if (propertyTypeId is null)
            {
                return _ => true;
            }
            
            return pr =>
                pr.PropertyTypeId.Equals(propertyTypeId);
        }
        
        internal static Expression<Func<Property, bool>> FilterByNumberOfRooms(int? numberOfRooms)
        {
            if (numberOfRooms is null)
            {
                return _ => true;
            }
            
            return pr =>
                pr.NumberOfRooms.Equals(numberOfRooms);
        }
        
        internal static Expression<Func<Property, bool>> FilterByNumberOfFloors(int? numberOfFloors)
        {
            if (numberOfFloors is null)
            {
                return _ => true;
            }
            
            return pr =>
                pr.NumberOfFloors.Equals(numberOfFloors);
        }
        
        internal static Expression<Func<Property, bool>> FilterByYearBuilt(int? minYearBuilt, int? maxYearBuilt)
        {
            if (minYearBuilt is null && maxYearBuilt is null)
            {
                return _ => true;
            }
            
            if (minYearBuilt is null)
            {
                return pr =>
                    pr.YearBuilt <= maxYearBuilt;
            }
            
            if (maxYearBuilt is null)
            {
                return pr =>
                    pr.YearBuilt >= minYearBuilt;
            }
            
            return pr =>
                pr.YearBuilt >= minYearBuilt && pr.YearBuilt <= maxYearBuilt;
        }
        
        internal static Expression<Func<Property, bool>> FilterByPlotArea(int? minPlotArea, int? maxPlotArea)
        {
            if (minPlotArea is null && maxPlotArea is null)
            {
                return _ => true;
            }
            
            if (minPlotArea is null)
            {
                return pr =>
                    pr.PlotArea <= maxPlotArea;
            }
            
            if (maxPlotArea is null)
            {
                return pr =>
                    pr.PlotArea >= minPlotArea;
            }
            
            return pr =>
                pr.PlotArea >= minPlotArea && pr.PlotArea <= maxPlotArea;
        }
        
        internal static Expression<Func<Property, bool>> FilterByFloorArea(int? minFloorArea, int? maxFloorArea)
        {
            if (minFloorArea is null && maxFloorArea is null)
            {
                return _ => true;
            }
            
            if (minFloorArea is null)
            {
                return pr =>
                    pr.FloorArea <= maxFloorArea;
            }
            
            if (maxFloorArea is null)
            {
                return pr =>
                    pr.FloorArea >= minFloorArea;
            }
            
            return pr =>
                pr.FloorArea >= minFloorArea && pr.FloorArea <= maxFloorArea;
        }
        
        internal static Expression<Func<Property, bool>> FilterByPrice(decimal? minPrice, decimal? maxPrice)
        {
            if (minPrice is null && maxPrice is null)
            {
                return _ => true;
            }
            
            if (minPrice is null)
            {
                return pr =>
                    pr.Price <= maxPrice;
            }
            
            if (maxPrice is null)
            {
                return pr =>
                    pr.Price >= minPrice;
            }
            
            return pr =>
                pr.Price >= minPrice && pr.Price <= maxPrice;
        }
    }
}