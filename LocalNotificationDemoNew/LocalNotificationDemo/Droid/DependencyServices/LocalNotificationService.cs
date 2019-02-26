﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.Media;
using Android.Support.V4.App;
using Android.Widget;
using Java.Lang;
using LocalNotificationDemo.DependencyServices.Droid;
using LocalNotificationDemo.Droid;
using LocalNotificationDemo.Models;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotificationService))]

namespace LocalNotificationDemo.DependencyServices.Droid{
    
    public class LocalNotificationService : ILocalNotificationService
    {
        int _notificationIconId { get; set; }
        readonly DateTime _jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        internal string _randomNumber;

        public void LocalNotification(string title, string body, int id, DateTime notifyTime){
            
            //long repeateDay = 1000 * 60 * 60 * 24;
            long repeateForMinute = 60000;
            long totalMilliSeconds = (long)(notifyTime.ToUniversalTime() - _jan1st1970).TotalMilliseconds;
            if (totalMilliSeconds < JavaSystem.CurrentTimeMillis()){
                totalMilliSeconds = totalMilliSeconds + repeateForMinute;
            }

            var intent = CreateIntent(id);
            var localNotification = new LocalNotification();
            localNotification.Title = title;
            localNotification.Body = body;
            localNotification.Id = id;
            localNotification.NotifyTime = notifyTime;

            if (_notificationIconId != 0){
                localNotification.IconId = _notificationIconId;
            }
            else{
                localNotification.IconId = Resource.Drawable.notificationgrey;
            }

            var serializedNotification = SerializeNotification(localNotification);
            intent.PutExtra(ScheduledAlarmHandler.LocalNotificationKey, serializedNotification);

            Random generator = new Random();  
            _randomNumber = generator.Next(100000, 999999).ToString("D6"); 

            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = GetAlarmManager();
            alarmManager.SetRepeating(AlarmType.RtcWakeup, totalMilliSeconds, repeateForMinute, pendingIntent);
        }
     
        public void Cancel(int id){
            
            var intent = CreateIntent(id);
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.Immutable);
            var alarmManager = GetAlarmManager();
            alarmManager.Cancel(pendingIntent);
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.CancelAll();
            notificationManager.Cancel(id);
        }

        public static Intent GetLauncherActivity(){
            
            var packageName = Application.Context.PackageName;
            return Application.Context.PackageManager.GetLaunchIntentForPackage(packageName);
        }


        private Intent CreateIntent(int id){
            
            return new Intent(Application.Context, typeof(ScheduledAlarmHandler))
                .SetAction("LocalNotifierIntent" + id);
        }

        private AlarmManager GetAlarmManager(){
            
            var alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            return alarmManager;
        }

        private string SerializeNotification(LocalNotification notification){
            
            var xmlSerializer = new XmlSerializer(notification.GetType());

            using (var stringWriter = new StringWriter()){
                xmlSerializer.Serialize(stringWriter, notification);
                return stringWriter.ToString();
            }
        }

        public void LocalNotificationList(string title, string body, int id, List<DateTime> notifyTime)
        {
            int i = 0;
            foreach (var item in notifyTime)
            {
                i++;
                long repeateForMinute = 60000;
                long totalMilliSeconds = (long)(item.ToUniversalTime() - _jan1st1970).TotalMilliseconds;
                if (totalMilliSeconds < JavaSystem.CurrentTimeMillis())
                {
                    totalMilliSeconds = totalMilliSeconds + repeateForMinute;
                }

                Calendar calendar = Calendar.Instance;

                DateTime dateTime = item.Date;
                dateTime.AddHours(item.Hour);
                dateTime.AddMinutes(item.Minute);
                dateTime.AddSeconds(00);

                calendar.Set(Calendar.Hour, dateTime.Hour);
                calendar.Set(Calendar.Minute, dateTime.Minute);
                calendar.Set(Calendar.Second, dateTime.Second);
                calendar.Set(Calendar.AmPm, Calendar.Pm);



                var intent = CreateIntent(i);
                var localNotification = new LocalNotification();
                localNotification.Title = title;
                localNotification.Body = body;
                localNotification.Id = i;
                localNotification.NotifyTime = item;

                if (_notificationIconId != 0)
                {
                    localNotification.IconId = _notificationIconId;
                }
                else
                {
                    localNotification.IconId = Resource.Drawable.notificationgrey;
                }

                
                var serializedNotification = SerializeNotification(localNotification);
                intent.PutExtra(ScheduledAlarmHandler.LocalNotificationKey, serializedNotification);

                Random generator = new Random();
                _randomNumber = generator.Next(100000, 999999).ToString("D6");

                var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.UpdateCurrent);
                var alarmManager = GetAlarmManager();
                Cancel(i);

                alarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);
            }
        }
    }

    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
    public class ScheduledAlarmHandler : BroadcastReceiver{

        public const string LocalNotificationKey = "LocalNotification";

        public override void OnReceive(Context context, Intent intent){
            var extra = intent.GetStringExtra(LocalNotificationKey);
            var view = new RemoteViews("", Resource.Layout.Tabbar);
            var notification = DeserializeNotification(extra);
            //Generating notification
            var builder = new NotificationCompat.Builder(Application.Context)
                .SetContentTitle(notification.Title)
                .SetContentText(notification.Body)
                .SetSmallIcon(notification.IconId)
                .SetCustomContentView(view)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Ringtone))
                .SetAutoCancel(true);

            var resultIntent = LocalNotificationService.GetLauncherActivity();
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            var stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(Application.Context);
            stackBuilder.AddNextIntent(resultIntent);

            Random random = new Random();
            int randomNumber = random.Next(9999 - 1000) + 1000; 

            var resultPendingIntent =
                stackBuilder.GetPendingIntent(randomNumber, (int)PendingIntentFlags.Immutable);
            builder.SetContentIntent(resultPendingIntent);
            // Sending notification
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(randomNumber, builder.Build());
        }

        private LocalNotification DeserializeNotification(string notificationString){
            
            var xmlSerializer = new XmlSerializer(typeof(LocalNotification));
            using (var stringReader = new StringReader(notificationString))
            {
                var notification = (LocalNotification)xmlSerializer.Deserialize(stringReader);
                return notification;
            }
        }
    }
}