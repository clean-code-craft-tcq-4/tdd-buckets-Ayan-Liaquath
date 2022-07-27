namespace BatteryMeasurements.Tests
{
    using System.Collections.Generic;

    public class MockChargingCurrentMeasurement : IChargingCurrentMeasurement
    {
        public int CurrentFunctionCallCount = 0;

        public IList<IRangeAndReadings> CalculateCurrentRangesAndReadings(List<int> currentRanges)
        {
            CurrentFunctionCallCount++;

            return null;
        }
    }
}
