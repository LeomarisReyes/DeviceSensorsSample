using System;
using System.Collections.Generic;
using Plugin.DeviceSensors;
using Plugin.DeviceSensors.Shared;
using DeviceSensorsSample.Models;
using System.ComponentModel;
using Xamarin.Forms; 
using DeviceSensorsSample.Views;
using static DeviceSensorsSample.Clases.Entities;

namespace DeviceSensorsSample.ViewModels
{
    public class DeviceSensorsViewModel   
    {
        public Sensors Sensor                 { get; set; }
        public List<Sensors>   ListDevices    { get; set; }
        public Command<string> ActionSensor   { get; set; } 
        public Command         StartThread    { get; set; }
        public Command         StopThread     { get; set; }
        public string          Time           { get; set; }

        public DeviceSensorsViewModel()
        {
            ActionSensor = new Command<string>(SelectedSensor); 
            FillListDevices();
        }

        public void FillListDevices()
		{
			ListDevices = new List<Sensors>();
            ListDevices.Add(new Sensors() { SensorName = "Acelerometer" });
            ListDevices.Add(new Sensors() { SensorName = "Barometer"     });
            ListDevices.Add(new Sensors() { SensorName = "Gyroscope"     });
            ListDevices.Add(new Sensors() { SensorName = "Magnetometer"  });
            ListDevices.Add(new Sensors() { SensorName = "Pedometer"     });                          
        }
         
        public async void SelectedSensor(string SensorName){ 
            
            if(SensorName == "Acelerometer")
            {
                await App.Navigation.PushAsync(new DeviceSensorDetailPage(SensorInformation.Acelerometer)); 
            }
            else if(SensorName == "Barometer")
            {
                await App.Navigation.PushAsync(new DeviceSensorDetailPage(SensorInformation.Barometer)); 
            } 
            else if(SensorName == "Gyroscope")
            {
                await App.Navigation.PushAsync(new DeviceSensorDetailPage(SensorInformation.Gyroscope)); 
            } 
            else if(SensorName == "Magnetometer")
            {
                await App.Navigation.PushAsync(new DeviceSensorDetailPage(SensorInformation.Magnetometer)); 
            } 
            else if(SensorName == "Pedometer")
            {
                await App.Navigation.PushAsync(new DeviceSensorDetailPage(SensorInformation.Pedometer)); 
            } 
        } 
          
}
}
