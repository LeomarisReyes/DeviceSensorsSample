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

            //BindingContext = new
            //{
            //    X = Sensor.X,
            //    Y = Sensor.Y,
            //    Z = Sensor.Y
            //};

            BindingContext = new
            {
                X = 0,
                Y = 0,
                Z = 0
            };

        }
    }
}
