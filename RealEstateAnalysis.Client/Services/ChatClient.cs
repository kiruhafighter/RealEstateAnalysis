namespace RealEstateAnalysis.Client.Services
{
    public interface IChatClient
    {
        Task<ChatResponse?> SendMessageAsync(string message, List<ChatMessage>? history = null);
    }

    public class ChatClient : IChatClient
    {
        private readonly HttpClient _httpClient;

        public ChatClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatResponse?> SendMessageAsync(string message, List<ChatMessage>? history = null)
        {
            var payload = new
            {
                message,
                history = history ?? new List<ChatMessage>()
            };

            var response = await _httpClient.PostAsJsonAsync("/chat", payload);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ChatResponse>();
        }
    }

    public class ChatMessage
    {
        public string role { get; set; } = default!;
        public string content { get; set; } = default!;
    }

    public class ChatResponse
    {
        public string? response { get; set; }
        public List<string>? suggested_property_ids { get; set; }
    }
}