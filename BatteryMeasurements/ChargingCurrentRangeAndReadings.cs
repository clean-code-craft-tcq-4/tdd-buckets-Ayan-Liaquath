namespace BatteryMeasurements
{
    public class ChargingCurrentRangeAndReadings : IRangeAndReadings
    {
        private readonly int _LowerLimit;

        private readonly int _UpperLimit;

        private readonly int _NumberOfReadings;

        public ChargingCurrentRangeAndReadings(int lowerLimit, int upperLimit, int numberOfReadings)
        {
            _LowerLimit = lowerLimit;
            _UpperLimit = upperLimit;
            _NumberOfReadings = numberOfReadings;
        }

        public int FetchLowerLimit()
        {
            return _LowerLimit;
        }

        public int FetchUpperLimit()
        {
            return _UpperLimit;
        }

        public int FetchNumberOfReadings()
        {
            return _NumberOfReadings;
        }
    }
}
