using System;
namespace DeviceSensorsSample.ViewModels
{
    public interface ISensorReader
    { 
        void StartReading(int milliseconds);
        void StopReading();
        bool IsReading { get; }
        int  Interval { get; set; }
    }
}
