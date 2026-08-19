using HRMS.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationResponse>> GetUserNotificationsAsync(int userId);
    Task MarkAsReadAsync(int notificationId);
    Task MarkAllAsReadAsync(int userId);
    Task CreateNotificationAsync(int userId, string message, string type);
}