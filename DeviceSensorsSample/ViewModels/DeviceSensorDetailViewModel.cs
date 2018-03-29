using System;
using System.Windows.Input;
using DeviceSensorsSample.Models;
using Plugin.DeviceSensors.Shared;
using Xamarin.Forms;

namespace DeviceSensorsSample.ViewModels
{
    public class DeviceSensorDetailViewModel<T> : ISensorReader
    {
        IDeviceSensor<T> sensor1;
        Action<T> action1;
        public Sensors  Sensor        { get; set; }
        public ICommand StartCommand  { get; set; } 
        public ICommand StopCommand   { get; set; } 
        public int      Interval      { get; set; } = 1;
        public bool     IsReading     { get { return sensor1.IsActive; } } 

        public DeviceSensorDetailViewModel(IDeviceSensor<T> sensor , Action<T> action)
        {
            sensor1 = sensor;
            action1 = action;
            StartCommand = new Command((p) => { StartReading(Interval * 1000); });
            StopCommand  = new Command(StopReading);
        }

        public void StartReading(int milliseconds)
        { 
            if (sensor1.IsSupported)
                { 
                sensor1.OnReadingChanged += (s, a) =>
                    {   
                        action1?.Invoke(a.Reading);
                    System.Diagnostics.Debug.WriteLine(a.Reading);
                    }; 
                sensor1.StartReading(milliseconds); 
                }   
        }

        public void StopReading()
        {
            sensor1.StopReading();  
        }

    }
}
