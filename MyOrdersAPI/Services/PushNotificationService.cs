using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MyOrdersAPI.Models;
using MyOrdersAPI.Services;

public class PushNotificationService : IPushNotificationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public PushNotificationService(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    public async Task<bool> SendNotificationAsync(PushNotificationRequest request)
    {
        var serverKey = _config["Firebase:ServerKey"];
        var senderId = _config["Firebase:SenderId"];

        var message = new
        {
            to = request.DeviceToken,
            notification = new
            {
                title = request.Title,
                body = request.Body
            },
            priority = "high"
        };

        var client = _httpClientFactory.CreateClient();
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send")
        {
            Content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json")
        };

        httpRequest.Headers.TryAddWithoutValidation("Authorization", $"key={serverKey}");
        httpRequest.Headers.TryAddWithoutValidation("Sender", $"id={senderId}");

        var response = await client.SendAsync(httpRequest);
        return response.IsSuccessStatusCode;
    }
}
