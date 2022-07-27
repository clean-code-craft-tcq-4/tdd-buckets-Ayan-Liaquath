namespace BatteryMeasurements.Tests
{
    using Xunit;

    public class BatteryMeasurementsTest
    {
        private readonly MockChargingCurrentMeasurement _ChargingCurrentMeasurement;

        private readonly MockFormatter _Formatter;

        private readonly MockSensorReadingProcessor _SensorReadingProcessor;

        private IBatteryMeasurements _BatteryMeasurements;
        
        private int _PrinterFunctionCall;

        public BatteryMeasurementsTest()
        {
            _ChargingCurrentMeasurement = new MockChargingCurrentMeasurement();
            _Formatter = new MockFormatter();
            _SensorReadingProcessor = new MockSensorReadingProcessor();

            _PrinterFunctionCall = 0;

            _BatteryMeasurements = new BatteryMeasurements(_ChargingCurrentMeasurement, _Formatter, _SensorReadingProcessor);
        }

        [Fact]
        public void TestDetectChargingCurrentMeasurementFlow()
        {
            _BatteryMeasurements.DetectChargingCurrentRangesAndReadings(null, PrinterFunction);

            DoAssertion(1,1,1, 1);
        }

        [Fact]
        public void TestDetectChargingCurrentMeasurementFlowWithInvalidPrinterFunction()
        {
            _BatteryMeasurements.DetectChargingCurrentRangesAndReadings(null, null);

            DoAssertion(1, 1, 0, 1);
        }

        [Fact]
        public void TestDetectChargingCurrentMeasurementFlowWithoutSensorProcessor()
        {
            _BatteryMeasurements = new BatteryMeasurements(_ChargingCurrentMeasurement, _Formatter, null);

            _BatteryMeasurements.DetectChargingCurrentRangesAndReadings(null, PrinterFunction);

            DoAssertion(1, 1, 1, 0);

        }

        private void DoAssertion(int currentFuncCallCount, int formatterFuncCallCount, int printerFuncCallCount, int sensorFuncCallCount)
        {
            Assert.Equal(currentFuncCallCount, _ChargingCurrentMeasurement.CurrentFunctionCallCount);

            Assert.Equal(formatterFuncCallCount, _Formatter.FunctionCallCount);

            Assert.Equal(printerFuncCallCount, _PrinterFunctionCall);

            Assert.Equal(sensorFuncCallCount, _SensorReadingProcessor.ProcessorFunctionCallCount);
        }

        private void PrinterFunction(string input)
        {
            Assert.Null(input);

            _PrinterFunctionCall++;
        }
    }
}
