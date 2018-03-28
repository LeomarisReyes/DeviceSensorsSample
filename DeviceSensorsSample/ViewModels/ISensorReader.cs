using System;
namespace DeviceSensorsSample.ViewModels
{
    public interface ISensorReader
    { 
        void StartReading(int seconds);
        void StopReading();
    }
}
