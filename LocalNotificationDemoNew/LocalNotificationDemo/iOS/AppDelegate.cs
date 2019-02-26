using Foundation;
using UIKit;

namespace LocalNotificationDemo.iOS{
    
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate{
        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options){
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            #region local notification


            // check for a notification
            if (options != null){

            if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey)){
                    UILocalNotification localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                    if (localNotification != null){
                        new UIAlertView(localNotification.AlertAction, localNotification.AlertBody, null, "OK", null).Show();
                        // reset our badge
                        UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                    }
                }
            }

            // We have checked to see if the device is running iOS 8, if so we are required to ask for the user's permission to receive notifications
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)){
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                );

                app.RegisterUserNotificationSettings(notificationSettings);
            }

            #endregion

            return base.FinishedLaunching(app, options);
        }



        #region Which will be called when a notification is received:

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification){
            
            UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
            okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }

        #endregion
    }
}
