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
        bool activeReading = false;

        public DeviceSensorDetailPage(SensorInformation dataSensor)
        { 
            switch (dataSensor)
            { 
                case SensorInformation.Acelerometer:
                    activeReader = new DeviceSensorDetailViewModel<VectorReading>(CrossDeviceSensors.Current.Accelerometer, (data) =>
                     { 
                        Reading.Text = $" X -> {data.X} - Y -> {data.Y} - Z ->  {data.Z}"; 
                     }); 
                    break;

                case SensorInformation.Magnetometer:
                    activeReader = new DeviceSensorDetailViewModel<VectorReading>(CrossDeviceSensors.Current.Magnetometer, (data) =>
                    {
                        Reading.Text = $" X -> {data.X} - Y -> {data.Y} - Z ->  {data.Z}"; 
                    }); 
                    break;

                case SensorInformation.Gyroscope:
                    activeReader = new DeviceSensorDetailViewModel<VectorReading>(CrossDeviceSensors.Current.Gyroscope, (data) =>
                    {
                        Reading.Text = $" X -> {data.X} - Y -> {data.Y} - Z ->  {data.Z}"; 
                    }); 
                    break;
                case SensorInformation.Barometer:
                    activeReader = new DeviceSensorDetailViewModel<double>(CrossDeviceSensors.Current.Barometer, (data) =>
                    {
                        Reading.Text = $"{data}"; 
                    });  
                    break;

                case SensorInformation.Pedometer:
                    activeReader = new DeviceSensorDetailViewModel<int>(CrossDeviceSensors.Current.Pedometer, (data) =>
                    {
                        Reading.Text = $"{data}"; 
                    }); 
                    break; 
            }  
            InitializeComponent();
            BindingContext = activeReader; 
        }


		protected override void OnAppearing()
		{
            base.OnAppearing();
            if (activeReading){
                activeReader.StartReading(activeReader.Interval);
            }
		}

		protected override void OnDisappearing()
		{
            base.OnDisappearing();
            activeReading = activeReader.IsReading;

            if (activeReading)
            {
                activeReader.StopReading(); 
            }
		}

        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            activeReader.Interval = (int)e.NewValue;
            intervalLabel.Text = $"{e.NewValue}";
        }

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
