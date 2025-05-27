using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateAnalysis.Client.Services;
using ApiClient;

namespace RealEstateAnalysis.Client.Pages
{
    public class ChatModel : PageModel
    {
        private readonly IChatClient _chatClient;
        private readonly IClient _client;

        public ChatModel(IChatClient chatClient, IClient client)
        {
            _chatClient = chatClient;
            _client = client;
        }

        [BindProperty]
        public string UserMessage { get; set; }

        [BindProperty]
        public List<ChatMessage> ChatHistory { get; set; } = new();

        public List<PropertyListedDto> SuggestedProperties { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            ChatHistory = new List<ChatMessage>();
            SuggestedProperties = new List<PropertyListedDto>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string clear)
        {
            if (Request.HasFormContentType && Request.Form.TryGetValue("ChatHistory", out var chatHistoryJson))
            {
                ChatHistory = System.Text.Json.JsonSerializer.Deserialize<List<ChatMessage>>(chatHistoryJson) ?? new List<ChatMessage>();
            }
            else
            {
                ChatHistory = new List<ChatMessage>();
            }

            if (!string.IsNullOrEmpty(clear))
            {
                ChatHistory.Clear();
                SuggestedProperties = new List<PropertyListedDto>();
                UserMessage = string.Empty;
                return Page();
            }
            
            if (!string.IsNullOrWhiteSpace(UserMessage))
            {
                ChatHistory.Add(new ChatMessage { role = "user", content = UserMessage });

                var response = await _chatClient.SendMessageAsync(UserMessage, ChatHistory);

                if (response?.response != null)
                {
                    ChatHistory.Add(new ChatMessage { role = "bot", content = response.response });
                }

                if (response?.suggested_property_ids != null && response.suggested_property_ids.Count > 0)
                {
                    var ids = response.suggested_property_ids
                        .Select(id => Guid.TryParse(id, out var guid) ? guid : (Guid?)null)
                        .Where(guid => guid.HasValue)
                        .Select(guid => guid.Value)
                        .ToList();

                    if (ids.Count > 0)
                    {
                        var props = await _client.GetListOfPropertiesByIdsAsync(new GetListOfPropertiesByIdsRequest
                        {
                            PropertyIds = ids
                        });

                        SuggestedProperties = props?.ToList() ?? new List<PropertyListedDto>();
                    }
                }
            }

            return Page();
        }
    }
}
