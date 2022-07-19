namespace BatteryMeasurements.Tests
{
    using System.Collections.Generic;

    public class MockFormatter : IFormatter<IRangeAndReadings>
    {
        public int FunctionCallCount = 0;

        public string Format(IList<IRangeAndReadings> inputToBeFormatted)
        {
            FunctionCallCount++;

            return null;
        }
    }
}
