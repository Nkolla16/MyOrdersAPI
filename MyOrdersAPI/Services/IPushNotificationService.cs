using System;
using MyOrdersAPI.Models;

namespace MyOrdersAPI.Services
{
    public interface IPushNotificationService
    {
        Task<bool> SendNotificationAsync(PushNotificationRequest request);
    }
}

