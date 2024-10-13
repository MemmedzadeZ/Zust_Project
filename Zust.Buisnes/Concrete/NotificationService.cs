using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.Entity.Data;
using Zust.Entity.Entities;

namespace Zust.Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly ZustDbContext _context;

        public NotificationService(ZustDbContext context)
        {
            _context = context;
        }


        public async Task<List<Notification>> GetUserNotifications(string userId)
        {
            return await _context.Notifications
                .Where(n => n.ReceiverId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotification(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }

        public async Task HideNotification(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
