namespace BatteryMeasurements.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class RangeAndReadingsFormatterTest
    {
        private readonly IFormatter<IRangeAndReadings> _Formatter;

        public RangeAndReadingsFormatterTest()
        {
            _Formatter = new RangeAndReadingsFormatter();
        }

        private void DoTest(IList<IRangeAndReadings> rangesList)
        {
            var testText = "Range, Readings\n";

            foreach (var range in rangesList)
            {
                testText += $"{range.FetchLowerLimit()}-{range.FetchUpperLimit()}, {range.FetchNumberOfReadings()}\n";
            }

            var formattedText = _Formatter.Format(rangesList);

            Assert.NotNull(formattedText);

            Assert.Equal(testText, formattedText);
        }

        [Fact]
        public void TestFormattingWithOneRangeAndReading()
        {
            DoTest(new List<IRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(2, 10, 5)
            });
        }

        [Fact]
        public void TestFormattingWithMultipleRangesAndReadings()
        {
            DoTest(new List<IRangeAndReadings>
            {
                new ChargingCurrentRangeAndReadings(2, 7, 5),
                new ChargingCurrentRangeAndReadings(12, 15, 4)
            });
        }

        [Fact]
        public void TestFormattingWithInvalidInput()
        {
            var formattedText = _Formatter.Format(null);

            Assert.Equal(string.Empty, formattedText);

            formattedText = _Formatter.Format(new List<IRangeAndReadings>());

            Assert.Equal(string.Empty, formattedText);
        }
    }
}
