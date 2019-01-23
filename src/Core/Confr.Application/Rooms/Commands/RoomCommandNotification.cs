using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Confr.Application.Interfaces;
using Confr.Application.Notifications.Models;

namespace Confr.Application.Rooms.Commands
{
    public class RoomCommandNotification : INotification
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public class RoomCommandNotificationHandler : INotificationHandler<RoomCommandNotification>
        {
            private readonly INotificationService _notificationService;

            public RoomCommandNotificationHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task Handle(RoomCommandNotification notification, CancellationToken cancellationToken)
            {
                await _notificationService.SendAsync(new Message
                {
                    To = notification.Email,
                    Subject = notification.Subject,
                    Body = notification.Body
                });
            }
        }
    }
}
