namespace BatteryMeasurements
{
    using System.Collections.Generic;

    public interface IFormatter<T>
    {
        string Format(IList<T> inputToBeFormatted);
    }
}
