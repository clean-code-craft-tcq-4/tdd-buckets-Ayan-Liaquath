namespace BatteryMeasurements
{
    public interface IRangeAndReadings
    {
        int FetchLowerLimit();

        int FetchUpperLimit();

        int FetchNumberOfReadings();
    }
}
