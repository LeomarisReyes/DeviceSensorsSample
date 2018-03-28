using System;
using System.Collections.Generic;
using System.ComponentModel;
using DeviceSensorsSample.ViewModels;
using Plugin.DeviceSensors;
using Plugin.DeviceSensors.Shared;
using Xamarin.Forms;
using static DeviceSensorsSample.Clases.Entities;

namespace DeviceSensorsSample.Views
{ 
    public partial class DeviceSensorDetailPage: INotifyPropertyChanged 
    {
        ISensorReader activeReader;
        public Command StartThread  { get; set; } 
        public Command StopThread   { get; set; }
        public int     Seconds      { get; set; }

        public DeviceSensorDetailPage(SensorInformation dataSensor)
        { 
            StartThread = new Command(Start);
            StopThread  = new Command(Stop);
             
            switch (dataSensor)
            { 
                case SensorInformation.Acelerometer:
                    activeReader = new DeviceSensorDetailViewModel<VectorReading>(CrossDeviceSensors.Current.Accelerometer, (data) =>
                     {
                        SetData($"{SensorInformation.Acelerometer}" , $" X -> {data.X} - Y -> {data.Y} - Z ->  {data.Z}");
                     }); 
                    break;

                case SensorInformation.Magnetometer:
                    activeReader = new DeviceSensorDetailViewModel<VectorReading>(CrossDeviceSensors.Current.Magnetometer, (data) =>
                    {
                        SetData($"{SensorInformation.Magnetometer}", $" X -> {data.X} - Y -> {data.Y} - Z ->  {data.Z}");
                    }); 
                    break;

                case SensorInformation.Gyroscope:
                    activeReader = new DeviceSensorDetailViewModel<VectorReading>(CrossDeviceSensors.Current.Gyroscope, (data) =>
                    {
                        SetData($"{SensorInformation.Gyroscope}", $" X -> {data.X} - Y -> {data.Y} - Z ->  {data.Z}");
                    }); 
                    break;
                case SensorInformation.Barometer:
                    activeReader = new DeviceSensorDetailViewModel<double>(CrossDeviceSensors.Current.Barometer, (data) =>
                    {
                        SetData($"{SensorInformation.Barometer}", $"{data}");
                    });  
                    break;

                case SensorInformation.Pedometer:
                    activeReader = new DeviceSensorDetailViewModel<int>(CrossDeviceSensors.Current.Pedometer, (data) =>
                    {
                        SetData($"{SensorInformation.Pedometer}", $"{data}"); 
                    });
                    activeReader.StartReading(500);
                    break; 
            }  
            InitializeComponent();  
            SetData("", "");
        } 

        public void SetData(string sensorname , string coordenates)
        {
            BindingContext = new
            {
                SensorName  = sensorname,
                Coordenates = coordenates , 
                Start       = StartThread,
                Stop        = StopThread
            };
        }
         
        public void Start()
        {    
            activeReader.StartReading(500);  
        }

        public void Stop()
        {
            activeReader.StopReading();
        } 

        public event PropertyChangedEventHandler PropertyChanged;
	}
}
