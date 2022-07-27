namespace BatteryMeasurements
{
    using System;
    using System.Collections.Generic;

    public class SensorReadingProcessor : ISensorReadingProcessor
    {
        private readonly int _MinimumReading;

        private readonly int _MinimumReadingValue;

        private readonly int _MaximumReading;

        private readonly int _MaximumReadingValue;

        public SensorReadingProcessor(int minimumReading, int minimumReadingValue, int maximumReading, int maximumReadingValue)
        {
            _MinimumReading = minimumReading;
            _MinimumReadingValue = minimumReadingValue;
            _MaximumReading = maximumReading;
            _MaximumReadingValue = maximumReadingValue;
        }

        public List<int> ProcessReadings(List<int> sensorReadings)
        {
            if (!CheckIfReadingsAreValidOrNot(sensorReadings))
            {
                return null;
            }

            return ConvertSensorReadingToActualReadings(sensorReadings);
        }

        private List<int> ConvertSensorReadingToActualReadings(List<int> sensorReadings)
        {
            var convertedReadings = new List<int>();

            sensorReadings.ForEach(sensorReading =>
            {
                if (!CheckIfErrorReading(sensorReading, _MinimumReadingValue, _MaximumReadingValue))
                {
                    var convertedReading =
                        ConvertSensorReadingToActualValue(sensorReading, _MaximumReading, _MaximumReadingValue, _MinimumReading);

                    convertedReadings.Add(convertedReading);
                }
            });

            return convertedReadings;
        }

        private int ConvertSensorReadingToActualValue(int sensorReading, int maximumReading, int maximumReadingValue, int minimumReading)
        {
            var sensorValue = (double) sensorReading / maximumReadingValue;

            var numberOfValuesInActualRange = maximumReading - minimumReading;

            return (int)Math.Abs(Math.Round(numberOfValuesInActualRange * sensorValue + minimumReading, MidpointRounding.AwayFromZero));
        }

        private bool CheckIfErrorReading(int sensorReading, int minimumReadingValue, int maximumReadingValue)
        {
            if (sensorReading < minimumReadingValue || sensorReading > maximumReadingValue)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfReadingsAreValidOrNot(List<int> sensorReadings)
        {
            return sensorReadings != null && sensorReadings.Count != 0;
        }
    }
}
