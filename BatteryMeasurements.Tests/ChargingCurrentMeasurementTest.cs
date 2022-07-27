namespace BatteryMeasurements.Tests
{
    using System.Collections.Generic;

    using Xunit;

    public class ChargingCurrentMeasurementTest
    {
        private readonly IChargingCurrentMeasurement _ChargingCurrentMeasurement;

        public ChargingCurrentMeasurementTest()
        {
            _ChargingCurrentMeasurement = new ChargingCurrentMeasurement();
        }


        private void PerformTest(List<int> currentSamples, IList<ChargingCurrentRangeAndReadings> rangeAndReadings)
        {
            var currentRanges = _ChargingCurrentMeasurement.CalculateCurrentRangesAndReadings(currentSamples);

            Assert.NotNull(currentRanges);

            Assert.Equal(rangeAndReadings.Count, currentRanges.Count);

            for (var index = 0; index < currentRanges.Count; index++)
            {
                Assert.Equal(rangeAndReadings[index].FetchLowerLimit(), currentRanges[index].FetchLowerLimit());

                Assert.Equal(rangeAndReadings[index].FetchUpperLimit(), currentRanges[index].FetchUpperLimit());

                Assert.Equal(rangeAndReadings[index].FetchNumberOfReadings(), currentRanges[index].FetchNumberOfReadings());
            }
        }

        [Fact]
        public void TestWithSortedSingleRange()
        {
            PerformTest(new List<int> { 4,5 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(4, 5, 2)
            });
        }

        [Fact]
        public void TestWithUnsortedSingleRange()
        {
            PerformTest(new List<int> { 7, 6 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(6, 7, 2)
            });
        }

        [Fact]
        public void TestWithSortedSingleRangeAndDuplicateSamples()
        {
            PerformTest(new List<int> { 8, 9, 9 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(8, 9, 3)
            });
        }

        [Fact]
        public void TestWithUnsortedSingleRangeAndDuplicateSamples()
        {
            PerformTest(new List<int> { 5, 4, 5 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(4, 5, 3)
            });
        }

        [Fact]
        public void TestWithSortedMultipleRanges()
        {
            PerformTest(new List<int> { 1, 2, 10, 11, 12, 100, 300 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(1, 2, 2),
                new ChargingCurrentRangeAndReadings(10, 12, 3),
                new ChargingCurrentRangeAndReadings(100, 100, 1),
                new ChargingCurrentRangeAndReadings(300, 300, 1)
            });
        }

        [Fact]
        public void TestWithUnsortedMultipleRanges()
        {
            PerformTest(new List<int> { 4, 3, 13, 14, 12, 400, 200 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(3, 4, 2),
                new ChargingCurrentRangeAndReadings(12, 14, 3),
                new ChargingCurrentRangeAndReadings(200, 200, 1),
                new ChargingCurrentRangeAndReadings(400, 400, 1)
            });
        }

        [Fact]
        public void TestWithSortedMultipleRangesAndDuplicateSamples()
        {
            PerformTest(new List<int> { 4, 5, 5, 5, 11, 11, 11, 10, 12, 100, 101 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(4, 5, 4),
                new ChargingCurrentRangeAndReadings(10, 12, 5),
                new ChargingCurrentRangeAndReadings(100, 101, 2),
            });
        }

        [Fact]
        public void TestWithNoRangeAndDuplicateSamples()
        {
            PerformTest(new List<int> { 80, 80, 80 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(80, 80, 3)
            });
        }

        [Fact]
        public void TestWithSingleSample()
        {
            PerformTest(new List<int> { 20 }, new List<ChargingCurrentRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(20, 20, 1)
            });
        }

        [Fact]
        public void TestWithInvalidSamples()
        {
            var ranges = _ChargingCurrentMeasurement.CalculateCurrentRangesAndReadings(null);

            Assert.Null(ranges);

            ranges = _ChargingCurrentMeasurement.CalculateCurrentRangesAndReadings(new List<int>());

            Assert.Null(ranges);
        }
    }
}
