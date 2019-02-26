using System;
using System.Collections.Generic;

namespace LocalNotificationDemo.DependencyServices
{
    public interface ILocalNotificationService
    {
        void LocalNotification(string title, string body, int id, DateTime notifyTime);
        void LocalNotificationList(string title, string body, int id, List<DateTime> notifyTime);
        void Cancel(int id);
    }
}