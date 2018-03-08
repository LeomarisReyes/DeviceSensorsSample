using System;
using System.Collections.Generic;
using Plugin.DeviceSensors;
using Plugin.DeviceSensors.Shared;
using DeviceSensorsSample.Models;
using System.ComponentModel;
using Xamarin.Forms;

namespace DeviceSensorsSample.ViewModels
{
    public class DeviceSensorsViewModel : INotifyPropertyChanged   
    {
        public List<Sensors>   ListDevices    { get; set; }
        public Command<string> ActionSensor   { get; set; }
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

        public void SelectedSensor(string SensorName){
             
            if(SensorName == "Acelerometer"){
                Acelerometer();
            }
			else if (SensorName == "Barometer")
			{
				Barometer();
			}
			else if (SensorName == "Gyroscope")
			{
				Gyroscope();
			}
			else if (SensorName == "Magnetometer")
			{
				Magnetometer();
			}
			else if (SensorName == "Pedometer")
			{
				Pedometer();
			}
        }

		public interface IDeviceSensor<T>
		{
			bool IsSupported { get; }                       // Check if sensor supported
			bool IsActive    { get; }                       // Check if sensor is active
			T    LastReading { get; }                      // Get latest sensor reading
			int  ReadingInterval { get; set; }             // Sets/get sensor report interval
			void StartReading(int readingInterval = -1);  // Starts sensor reading
			void StopReading();                           // Stops sensor reading
			event EventHandler<DeviceSensorReadingEventArgs<T>> OnReadingChanged; // Sensor reading changes event
		}


		public interface IDeviceSensors
		{
			IDeviceSensor<VectorReading>        Accelerometer { get; }
			IDeviceSensor<VectorReading>        Gyroscope     { get; }
			IDeviceSensor<VectorReading>        Magnetometer  { get; }
			IDeviceSensor<double>               Barometer     { get; }
			IDeviceSensor<int>                  Pedometer     { get; }
		}

        public void Acelerometer (){
			if (CrossDeviceSensors.Current.Accelerometer.IsSupported)
			{
				CrossDeviceSensors.Current.Accelerometer.OnReadingChanged += (s, a) =>
				{
                    System.Diagnostics.Debug.WriteLine(" OnReadingChanged - Acelerometer   : X -> " + a.Reading.X + " Y-> " + a.Reading.Y + " Z -> " + a.Reading.Z);
				};
				CrossDeviceSensors.Current.Accelerometer.StartReading();
                System.Diagnostics.Debug.WriteLine(" StartReading");

			}
			System.Diagnostics.Debug.WriteLine(" Acelerometer");

		}

		public void Gyroscope()
		{
			if (CrossDeviceSensors.Current.Gyroscope.IsSupported)
			{
				CrossDeviceSensors.Current.Gyroscope.OnReadingChanged += (s, a) =>
				{
					System.Diagnostics.Debug.WriteLine("OnReadingChanged: Gyroscope  X -> " + a.Reading.X + " Y-> " + a.Reading.Y + " Z -> " + a.Reading.Z);
				};
				CrossDeviceSensors.Current.Gyroscope.StartReading();
				System.Diagnostics.Debug.WriteLine("StartReading: Gyroscope ");

			}
            System.Diagnostics.Debug.WriteLine(" Gyroscope");
		}

		public void Magnetometer()
		{
            if (CrossDeviceSensors.Current.Magnetometer.IsSupported)
			{
                CrossDeviceSensors.Current.Magnetometer.OnReadingChanged += (s, a) =>
				{
					System.Diagnostics.Debug.WriteLine("OnReadingChanged: Magnetometer  X -> " + a.Reading.X + " Y-> " + a.Reading.Y + " Z -> " + a.Reading.Z);
				};
                CrossDeviceSensors.Current.Magnetometer.StartReading();
				System.Diagnostics.Debug.WriteLine("StartReading: Magnetometer");

			}
            System.Diagnostics.Debug.WriteLine(" Magnetometer");
		}

		public void Barometer()
		{
            if (CrossDeviceSensors.Current.Barometer.IsSupported)
			{
                CrossDeviceSensors.Current.Barometer.OnReadingChanged += (s, a) =>
				{
                    System.Diagnostics.Debug.WriteLine("OnReadingChanged: Barometer -> " + a.Reading.ToString());
				};
                CrossDeviceSensors.Current.Barometer.StartReading();
				System.Diagnostics.Debug.WriteLine("StartReading: Barometer ");

			}
			System.Diagnostics.Debug.WriteLine(" Barometer");
		}

		public void Pedometer()
		{
			if (CrossDeviceSensors.Current.Pedometer.IsSupported)
			{
				CrossDeviceSensors.Current.Pedometer.OnReadingChanged += (s, a) =>
				{
					System.Diagnostics.Debug.WriteLine("OnReadingChanged: Pedometer -> " + a.Reading.ToString());
				};
				CrossDeviceSensors.Current.Pedometer.StartReading();
				System.Diagnostics.Debug.WriteLine("StartReading: Pedometer");

			}
            System.Diagnostics.Debug.WriteLine(" Pedometer");
		}

        public event PropertyChangedEventHandler PropertyChanged;
}
}
