using System;
using DeviceSensorsSample.Models;
using Plugin.DeviceSensors.Shared;

namespace DeviceSensorsSample.ViewModels
{
    public class DeviceSensorDetailViewModel<T> : ISensorReader
    {
        IDeviceSensor<T> sensor1;
        Action<T> action1;
        public Sensors Sensor { get; set; }

        public DeviceSensorDetailViewModel(IDeviceSensor<T> sensor , Action<T> action)
        {
            sensor1 = sensor;
            action1 = action;
        }

        public void StartReading(int seconds)
        {
            if (sensor1.IsSupported)
                { 
                sensor1.OnReadingChanged += (s, a) =>
                    {   
                        action1?.Invoke(a.Reading);     
                    }; 
                sensor1.StartReading(seconds); 
                }  
        }

        public void StopReading()
        {
            sensor1.StopReading(); 
        }

    }
}
