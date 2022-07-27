namespace BatteryMeasurements
{
    using System;
    using System.Collections.Generic;

    public class BatteryMeasurements : IBatteryMeasurements
    {
        private readonly IChargingCurrentMeasurement _ChargingCurrentMeasurement;

        private readonly IFormatter<IRangeAndReadings> _Formatter;

        private readonly ISensorReadingProcessor _SensorReadingProcessor;

        public BatteryMeasurements(
            IChargingCurrentMeasurement chargingCurrentMeasurement,
            IFormatter<IRangeAndReadings> formatter,
            ISensorReadingProcessor sensorReadingProcessor = null)
        {
            _ChargingCurrentMeasurement = chargingCurrentMeasurement;
            _Formatter = formatter;
            _SensorReadingProcessor = sensorReadingProcessor;
        }

        public void DetectChargingCurrentRangesAndReadings(List<int> currentSamples, Action<string> printerAction)
        {
            if (_SensorReadingProcessor != null)
            {
                currentSamples = _SensorReadingProcessor.ProcessReadings(currentSamples);
            }

            var rangesAndReadings = _ChargingCurrentMeasurement.CalculateCurrentRangesAndReadings(currentSamples);

            var formattedText = _Formatter.Format(rangesAndReadings);

            printerAction?.Invoke(formattedText);
        }
    }
}
