namespace BatteryMeasurements
{
    using System.Collections.Generic;

    public interface ISensorReadingProcessor
    {
        List<int> ProcessReadings(List<int> sensorReadings);
    }
}
