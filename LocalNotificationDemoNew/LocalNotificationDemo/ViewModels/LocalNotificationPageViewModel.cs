﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using LocalNotificationDemo.DependencyServices;
using Xamarin.Forms;

namespace LocalNotificationDemo.ViewModels{
    
    public class LocalNotificationPageViewModel : INotifyPropertyChanged{
        
        Command _saveCommand;
        Command _savetime;

        public Command SaveTime
        {
            get
            {
                return _savetime;
            }
            set
            {
                SetProperty(ref _savetime, value);
            }
        }
        public Command SaveCommand{
            get{
                return _saveCommand;
            }
            set{
                SetProperty(ref _saveCommand, value);
            }
        }

        bool _notificationONOFF;
        public bool NotificationONOFF{
            get{
                return _notificationONOFF;
            }
            set{
                SetProperty(ref _notificationONOFF, value);
                Switch_Toggled();
            }
        }

        void Switch_Toggled(){

            if (NotificationONOFF == false){
                
                MessageText = string.Empty;
                SelectedTime = DateTime.Now.TimeOfDay;
                SelectedDate = DateTime.Today;
                DependencyService.Get<ILocalNotificationService>().Cancel(0);
            }
        }

        DateTime _selectedDate=DateTime.Today;
        public DateTime SelectedDate{
            get{
                return _selectedDate;
            }
            set{
                SetProperty(ref _selectedDate, value);
            }
        }

        TimeSpan _selectedTime=DateTime.Now.TimeOfDay;
        public TimeSpan SelectedTime{
            get{
                return _selectedTime;
            }
            set{
                SetProperty(ref _selectedTime, value);
            }
        }

        string _messageText;
        public string MessageText{
            get{
                return _messageText;
            }
            set{
                SetProperty(ref _messageText, value);
            }
        }

        public LocalNotificationPageViewModel(){
            SaveCommand = new Command(() => SaveLocalNotification());
            SaveTime = new Command(() => SaveLocalNotificationList());
        }

        void SaveLocalNotification(){
            
            if(NotificationONOFF==true){

                //var date = (SelectedDate.Date.Month.ToString("00") + "-" + SelectedDate.Date.Day.ToString("00") + "-" + SelectedDate.Date.Year.ToString());

                //var time = Convert.ToDateTime(SelectedTime.ToString()).ToString("HH:mm");

                //var dateTime = date + " " + time;

                //var selectedDateTime = DateTime.ParseExact(dateTime, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
                List<DateTime> selectedDateTime=new List<DateTime>();

                foreach (var item in SaveLocalNotificationList())
                {
                    var date = (SelectedDate.Date.Month.ToString("00") + "-" + SelectedDate.Date.Day.ToString("00") + "-" + SelectedDate.Date.Year.ToString());

                    var time = Convert.ToDateTime(item.ToString()).ToString("HH:mm");
                    
                    var dateTime = date + " " + time;

                    selectedDateTime.Add(DateTime.ParseExact(dateTime, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture));
                }
                


                if (!string.IsNullOrEmpty(MessageText)){
                    
                    DependencyService.Get<ILocalNotificationService>().Cancel(0);
                    //DependencyService.Get<ILocalNotificationService>().LocalNotification("Local Notification", MessageText, 0, selectedDateTime);
                    DependencyService.Get<ILocalNotificationService>().LocalNotificationList("Local Notification", MessageText, 0, selectedDateTime);
                    App.Current.MainPage.DisplayAlert("LocalNotificationDemo", "Notification details saved successfully ", "Ok");

                }else{
                    App.Current.MainPage.DisplayAlert("LocalNotificationDemo", "Please enter meassage", "OK");
                }

            }else{
                App.Current.MainPage.DisplayAlert("LocalNotificationDemo", "Please switch on notification", "OK");
            }
        }
        List<string> lst = new List<string>();
        List<string> SaveLocalNotificationList()
        {

            lst.Add(Convert.ToDateTime(SelectedTime.ToString()).ToString("HH:mm"));
            return lst;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName]string propertyName = "",
           Action onChanged = null){
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = ""){
            var changed = PropertyChanged;
            if (changed == null)
                return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}