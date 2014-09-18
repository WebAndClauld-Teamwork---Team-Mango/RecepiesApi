namespace RecepiesApp.Services.Notifications
{
    using System.Collections.Generic;

    public interface INotifier
    {
        string Notify(string user, object message);
    }
}