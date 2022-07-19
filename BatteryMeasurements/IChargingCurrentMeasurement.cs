namespace BatteryMeasurements
{
    using System.Collections.Generic;

    public interface IChargingCurrentMeasurement
    {
        IList<IRangeAndReadings> CalculateCurrentRangesAndReadings(List<int> currentRanges);
    }
}
