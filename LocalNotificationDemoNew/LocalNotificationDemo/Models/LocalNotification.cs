﻿using System;
namespace LocalNotificationDemo.Models{
    
    public class LocalNotification{

        public string Title { get; set; }

        public string Body { get; set; }

        public int Id { get; set; }

        public int IconId { get; set; }

        public DateTime NotifyTime { get; set; }
    }
}