using System.Collections.Generic;
using DeviceSensorsSample.Models;
using DeviceSensorsSample.ViewModels;
using Xamarin.Forms;

namespace DeviceSensorsSample
{
    public partial class DeviceSensorsSamplePage : ContentPage
    {  
        public DeviceSensorsSamplePage()
        {
            InitializeComponent();
            BindingContext = new DeviceSensorsViewModel();
             
            if(Device.RuntimePlatform == Device.iOS)
            {
                this.Padding = new Thickness(0, 20, 0, 0);
            }
        }
    }
}
