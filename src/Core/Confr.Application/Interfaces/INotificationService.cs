using Confr.Application.Notifications.Models;
using System.Threading.Tasks;

namespace Confr.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
