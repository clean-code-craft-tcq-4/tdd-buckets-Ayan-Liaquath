namespace BatteryMeasurements.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class SensorReadingProcessorTest
    {
        private void PerformTest(ISensorReadingProcessor sensorReadingProcessor, List<int> sensorReadings, List<int> actualReadings)
        {
            var convertedReadings = sensorReadingProcessor.ProcessReadings(sensorReadings);

            Assert.NotNull(convertedReadings);

            Assert.Equal(actualReadings.Count, convertedReadings.Count);

            for (var index = 0; index < convertedReadings.Count; index++)
            {
                Assert.Equal(actualReadings[index], convertedReadings[index]);
            }
        }

        [Fact]
        public void Process12BitA2DConverterChargingReadings()
        {
            PerformTest(new SensorReadingProcessor(0, 0, 10, 4094),
                new List<int> { 45, 0, 4094, 1024 },
                new List<int> { 0, 0, 10, 3 });
        }

        [Fact]
        public void Process12BitA2DConverterChargingReadingsWithErrorReadings()
        {
            PerformTest(new SensorReadingProcessor(0, 0, 10, 4094),
                new List<int> { 1000, 20, 4095, 4095 },
                new List<int> { 2, 0 });
        }

        [Fact]
        public void Process12BitA2DConverterChargingReadingsWithDuplicateReadings()
        {
            PerformTest(new SensorReadingProcessor(0, 0, 10, 4094),
                new List<int> { 1000, -1, 0, 1000, 60 },
                new List<int> { 2, 0, 2, 0 });
        }

        [Fact]
        public void ProcessSensorReadingsWithInvalidInput()
        {
            var sensorReadingProcessor = new SensorReadingProcessor(0, 0, 10, 4094);

            var convertedReadings = sensorReadingProcessor.ProcessReadings(null);

            Assert.Null(convertedReadings);

            convertedReadings = sensorReadingProcessor.ProcessReadings(new List<int>());

            Assert.Null(convertedReadings);
        }

        [Fact]
        public void Process10BitA2DConverterChargingAndDischargingReadings()
        {
            PerformTest(new SensorReadingProcessor(-15, 0, 15, 1022),
                new List<int> { 45, 0, 1022, 511 },
                new List<int> { 14, 15, 15, 0 });
        }

        [Fact]
        public void Process10BitA2DConverterChargingAndDischargingReadingsWithErrorReadings()
        {
            PerformTest(new SensorReadingProcessor(-15, 0, 15, 1022),
                new List<int> { 1023, 206, 1023, 625 },
                new List<int> { 9, 3 });
        }

        [Fact]
        public void Process10BitA2DConverterChargingAndDischargingReadingsWithDuplicateReadings()
        {
            PerformTest(new SensorReadingProcessor(-15, 0, 15, 1022),
                new List<int> { 0, 500, 0, 1000, 90 },
                new List<int> { 15, 0, 15, 14, 12 });
        }
    }
}
