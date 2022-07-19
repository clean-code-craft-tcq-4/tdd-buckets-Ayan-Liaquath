namespace BatteryMeasurements
{
    using System;
    using System.Collections.Generic;

    public class BatteryMeasurements : IBatteryMeasurements
    {
        private readonly IChargingCurrentMeasurement _ChargingCurrentMeasurement;

        private readonly IFormatter<IRangeAndReadings> _Formatter;

        public BatteryMeasurements(IChargingCurrentMeasurement chargingCurrentMeasurement, IFormatter<IRangeAndReadings> formatter)
        {
            _ChargingCurrentMeasurement = chargingCurrentMeasurement;
            _Formatter = formatter;
        }

        public void DetectChargingCurrentRangesAndReadings(List<int> currentSamples, Action<string> printerAction)
        {
            var rangesAndReadings = _ChargingCurrentMeasurement.CalculateCurrentRangesAndReadings(currentSamples);

            var formattedText = _Formatter.Format(rangesAndReadings);

            printerAction?.Invoke(formattedText);
        }
    }
}
