namespace BatteryMeasurements
{
    using System.Collections.Generic;

    public class RangeAndReadingsFormatter: IFormatter<IRangeAndReadings>
    {
        public string Format(IList<IRangeAndReadings> rangeAndReadingsList)
        {
            if (!CheckIfInputToBeFormattedIsValidOrNot(rangeAndReadingsList))
            {
                return string.Empty;
            }

            return ConstructFormattedString(rangeAndReadingsList);
        }

        private static bool CheckIfInputToBeFormattedIsValidOrNot(IList<IRangeAndReadings> input)
        {
            return input != null && input.Count > 0;
        }

        private static string ConstructFormattedString(IList<IRangeAndReadings> rangeAndReadingsList)
        {
            var formattedString = "Range, Readings\n";

            foreach (var rangeAndReadings in rangeAndReadingsList)
            {
                formattedString +=
                    $"{rangeAndReadings.FetchLowerLimit()}-{rangeAndReadings.FetchUpperLimit()}, {rangeAndReadings.FetchNumberOfReadings()}\n";
            }

            return formattedString;
        }
    }
}
