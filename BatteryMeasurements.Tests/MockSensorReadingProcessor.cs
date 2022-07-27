namespace BatteryMeasurements.Tests
{
    using System.Collections.Generic;

    public class MockSensorReadingProcessor : ISensorReadingProcessor
    {
        public int ProcessorFunctionCallCount = 0;

        public List<int> ProcessReadings(List<int> sensorReadings)
        {
            ProcessorFunctionCallCount++;

            return null;
        }
    }
}
