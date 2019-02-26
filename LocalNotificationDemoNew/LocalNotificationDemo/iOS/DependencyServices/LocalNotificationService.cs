﻿using System;
using UIKit;
using Foundation;
using System.Linq;
using Xamarin.Forms;
using LocalNotificationDemo.iOS;
using LocalNotificationDemo.DependencyServices;
using System.Collections.Generic;

[assembly: Dependency(typeof(LocalNotificationService))]
namespace LocalNotificationDemo.iOS{
    
    public class LocalNotificationService : ILocalNotificationService{
        
        const string NotificationKey = "LocalNotificationKey";

        public void LocalNotification(string title, string body, int id, DateTime notifyTime){
            
            var notification = new UILocalNotification{
                
                AlertTitle = title,
                AlertBody = body,
                SoundName = UILocalNotification.DefaultSoundName,
                FireDate = notifyTime.ToNSDate(),
                RepeatInterval = NSCalendarUnit.Minute,

                UserInfo = NSDictionary.FromObjectAndKey(NSObject.FromObject(id), NSObject.FromObject(NotificationKey))
            };
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }

        public void Cancel(int id){
            
            var notifications = UIApplication.SharedApplication.ScheduledLocalNotifications;
            var notification = notifications.Where(n => n.UserInfo.ContainsKey(NSObject.FromObject(NotificationKey)))
                .FirstOrDefault(n => n.UserInfo[NotificationKey].Equals(NSObject.FromObject(id)));
            UIApplication.SharedApplication.CancelAllLocalNotifications();
            if (notification != null){
                UIApplication.SharedApplication.CancelLocalNotification(notification);
                UIApplication.SharedApplication.CancelAllLocalNotifications();
            }
        }

        public void LocalNotificationList(string title, string body, int id, List<DateTime> notifyTime)
        {
            throw new NotImplementedException();
        }
    }

    public static class DateTimeExtensions{
        
        static DateTime nsUtcRef = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        // last zero is milliseconds

        public static double SecondsSinceNSRefenceDate(this DateTime dt){
            return (dt.ToUniversalTime() - nsUtcRef).TotalSeconds;
        }

        public static NSDate ToNSDate(this DateTime dt){
            return NSDate.FromTimeIntervalSinceReferenceDate(dt.SecondsSinceNSRefenceDate());
        }
    }
}