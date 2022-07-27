namespace BatteryMeasurements
{
    using System.Collections.Generic;
    using System.Linq;
    
    public class ChargingCurrentMeasurement : IChargingCurrentMeasurement
    {
        public IList<IRangeAndReadings> CalculateCurrentRangesAndReadings(List<int> currentRanges)
        {
            if (!CheckIfSamplesAreValidOrNot(currentRanges))
            {
                return null;
            }

            return CalculateRangesAndReadings(currentRanges);
        }

        private static IList<IRangeAndReadings> CalculateRangesAndReadings(List<int> currentRanges)
        {
            var currentRangesAndReadings = new List<IRangeAndReadings>();

            currentRanges = SortList(currentRanges);

            var lowerLimit = currentRanges.First();

            var upperLimit = lowerLimit;

            var numberOfReadings = 1;

            for (var index = 1; index < currentRanges.Count; index++)
            {
                if (CheckUpperLimit(currentRanges[index], upperLimit))
                {
                    upperLimit = currentRanges[index];
                    numberOfReadings++;
                }
                else
                {
                    currentRangesAndReadings.Add(new ChargingCurrentRangeAndReadings(lowerLimit, upperLimit, numberOfReadings));

                    lowerLimit = currentRanges[index];
                    upperLimit = lowerLimit;
                    numberOfReadings = 1;
                }
            }

            currentRangesAndReadings.Add(new ChargingCurrentRangeAndReadings(lowerLimit, upperLimit, numberOfReadings));

            return currentRangesAndReadings;
        }

        private static List<int> SortList(List<int> unsortedList)
        {
            unsortedList?.Sort();

            return unsortedList;
        }

        private static bool CheckUpperLimit(int currentSample, int upperLimit)
        {
            return currentSample == upperLimit + 1 || currentSample == upperLimit;
        }

        private bool CheckIfSamplesAreValidOrNot(List<int> currentRanges)
        {
            return currentRanges != null && currentRanges.Count != 0;
        }
    }
}
