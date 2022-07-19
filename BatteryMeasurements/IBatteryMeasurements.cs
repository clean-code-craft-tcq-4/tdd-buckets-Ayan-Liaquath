namespace BatteryMeasurements
{
    using System;
    using System.Collections.Generic;

    public interface IBatteryMeasurements
    {
        void DetectChargingCurrentRangesAndReadings(List<int> currentSamples, Action<string> printerAction);
    }
}
