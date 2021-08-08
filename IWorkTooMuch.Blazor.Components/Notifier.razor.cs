using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IWorkTooMuch.Blazor.Components
{
    public struct Notification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }

    public enum NotificationType
    {
        Default,
        Error,
        Warning,
        Success
    }

    public partial class Notifier
    {
        [Inject]
        private ILogger<Notifier> Logger { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public List<Notification> Notifications { get; set; } = new();

        public void ProcessSuccess(string title, string message)
        {
            Notification notification = new()
            {
                Title = title,
                Message = message,
                Type = NotificationType.Success
            };

            Notifications.Add(notification);

            StateHasChanged();

            InvokeAsync(async () => await CloseAfterDelay(notification));
        }

        public void ProcessNotification(string title, string message)
        {
            Notification notification = new()
            {
                Title = title,
                Message = message,
                Type = NotificationType.Default
            };

            Notifications.Add(notification);

            StateHasChanged();

            InvokeAsync(async () => await CloseAfterDelay(notification));
        }

        public void ProcessWarning(string title, string message)
        {
            Notification notification = new()
            {
                Title = title,
                Message = message,
                Type = NotificationType.Warning
            };

            Notifications.Add(notification);

            Logger.LogWarning("Warning:ProcessError - Type: {title} Message: {message}",
                title, message);

            StateHasChanged();

            InvokeAsync(async () => await CloseAfterDelay(notification));
        }

        public void ProcessError(Exception ex)
        {
            Notification notification = new()
            {
                Title = ex.GetType().ToString(),
                Message = ex.Message,
                Type = NotificationType.Error
            };

            Notifications.Add(notification);

            Logger.LogError("Error:ProcessError - Type: {Type} Message: {Message}",
                ex.GetType(), ex.Message);

            StateHasChanged();

            InvokeAsync(async () => await CloseAfterDelay(notification));
        }

        private void Close(Notification notification)
        {
            try
            {
                if (Notifications.Contains(notification))
                    Notifications.Remove(notification);
            }
            catch { }
        }

        private async Task CloseAfterDelay(Notification notification, int seconds = 5)
        {
            await Task.Delay(seconds * 1000);
            Close(notification);
            StateHasChanged();
        }

        private string NotificationTypeClass(Notification notification) => notification.Type switch
        {
            NotificationType.Error => "error-box",
            NotificationType.Warning => "warning-box",
            NotificationType.Success => "success-box",
            _ => "notify-box",
        };
    }
}