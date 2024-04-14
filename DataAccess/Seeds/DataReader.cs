using System.Reflection;
using System.Text.Json;

namespace DataAccess.Seeds;

internal static class DataReader<T>
{
    internal static List<T> Read(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceFullName = $"{assembly.GetName().Name}.{resourceName}";
        
        using var stream = assembly.GetManifestResourceStream(resourceFullName);
        
        using StreamReader reader = new(stream!);

        while (!reader.EndOfStream)
        {
            string text = reader.ReadToEnd();
            
            var items = JsonSerializer.Deserialize<List<T>>(text);

            if (items is not null)
            {
                return items;
            }
        }
        
        return new List<T>();
    }
}