using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entity.Entities;

namespace Zust.Business.Abstract
{
    public interface INotificationService
    {
        Task<List<Notification>> GetUserNotifications(string userId);
        Task DeleteNotification(int notificationId);
        Task HideNotification(int notificationId);
    }
}
