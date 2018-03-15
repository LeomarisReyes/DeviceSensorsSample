using System;
using System.Collections.Generic;
using DeviceSensorsSample.Models;
using Xamarin.Forms;

namespace DeviceSensorsSample.Views
{
    public partial class SensorDetailsPage : ContentPage
    {
        public SensorDetailsPage(Sensors Sensor)
        {
            InitializeComponent(); 

            BindingContext = new
            {
                SensorName = Sensor.SensorName,
                X          = Sensor.X,
                Y          = Sensor.Y,
                Z          = Sensor.Y
            }; 
             
        }
    }
}
