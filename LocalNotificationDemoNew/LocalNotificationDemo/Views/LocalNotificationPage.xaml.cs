using LocalNotificationDemo.ViewModels;
using Xamarin.Forms;

namespace LocalNotificationDemo.Views
{
    public partial class LocalNotificationPage : ContentPage
    {
        public LocalNotificationPage()
        {
            InitializeComponent();
            BindingContext = new LocalNotificationPageViewModel();
        }
    }
}