using System;
using System.Collections.Generic;
using Plugin.DeviceSensors;
using Plugin.DeviceSensors.Shared;
using DeviceSensorsSample.Models;
using System.ComponentModel;
using Xamarin.Forms;
using DeviceSensorsSample.Views;
using System.Threading.Tasks;
using System.Threading;

namespace DeviceSensorsSample.ViewModels
{
    public class DeviceSensorsViewModel : INotifyPropertyChanged   
    {
        public Sensors Sensor                 { get; set; }
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
                StopThreads();  
                if (CrossDeviceSensors.Current.Accelerometer.IsSupported)
                { 
                    CrossDeviceSensors.Current.Accelerometer.OnReadingChanged += (s, a) =>
                    {
                        FillData("Acelerometer" , a.Reading.X , a.Reading.Y , a.Reading.Z); 
                    }; 
                    CrossDeviceSensors.Current.Accelerometer.StartReading(3000); 
                } 
		}

		public void Gyroscope()
		{ 
            StopThreads(); 
			if (CrossDeviceSensors.Current.Gyroscope.IsSupported)
			{
				CrossDeviceSensors.Current.Gyroscope.OnReadingChanged += (s, a) =>
				{ 
                    FillData("Gyroscope", a.Reading.X, a.Reading.Y, a.Reading.Z); 
				};  
				CrossDeviceSensors.Current.Gyroscope.StartReading(3000); 
			} 
		}

		public void Magnetometer()
		{
            StopThreads(); 
            if (CrossDeviceSensors.Current.Magnetometer.IsSupported)
			{
                CrossDeviceSensors.Current.Magnetometer.OnReadingChanged += (s, a) =>
				{ 
                    FillData("Magnetometer" , a.Reading.X , a.Reading.Y , a.Reading.Z); 
				};  
                CrossDeviceSensors.Current.Magnetometer.StartReading(3000); 
			} 
		}

		public void Barometer()
		{
            StopThreads();
            if (CrossDeviceSensors.Current.Barometer.IsSupported)
			{
                CrossDeviceSensors.Current.Barometer.OnReadingChanged += (s, a) =>
				{ 
                    FillData("Barometer" , a.Reading , 0 , 0); 
				}; 
                CrossDeviceSensors.Current.Barometer.StartReading(3000); 
			} 
		}

		public void Pedometer()
		{
            StopThreads();
			if (CrossDeviceSensors.Current.Pedometer.IsSupported)
			{
				CrossDeviceSensors.Current.Pedometer.OnReadingChanged += (s, a) =>
				{ 
                    FillData("Pedometer" , a.Reading , 0 , 0); 
				}; 
				CrossDeviceSensors.Current.Pedometer.StartReading(3000); 
			}
		}

        public async void FillData(string SensorDescription, double XX , double YY , double ZZ){
            Sensor = new Sensors() { 
                SensorName     = SensorDescription, 
                    X          = XX, 
                    Y          = YY, 
                    Z          = ZZ 
                                   };
            await App.Navigation.PushAsync(new SensorDetailsPage(Sensor));           
        }

        public void StopThreads(){
            CrossDeviceSensors.Current.Accelerometer.StopReading();
            CrossDeviceSensors.Current.Gyroscope.StopReading();
            CrossDeviceSensors.Current.Magnetometer.StopReading();
            CrossDeviceSensors.Current.Barometer.StopReading();
            CrossDeviceSensors.Current.Pedometer.StopReading();
        }
         
        public event PropertyChangedEventHandler PropertyChanged;
}
}
