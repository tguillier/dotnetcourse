namespace ProductImporter.Logic.Transformations.Util;

public class ReferenceGenerator : IReferenceGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IInternalCounter _counter;

    public ReferenceGenerator(
        IDateTimeProvider dateTimeProvider,
        IInternalCounter counter)
    {
        _dateTimeProvider = dateTimeProvider;
        _counter = counter;
    }

    public string GetReference()
    {
        var dateTime = _dateTimeProvider.GetUtcDateTime();
        var counter = _counter.GetNextValue();

        var reference = $"{dateTime:yyyy-MM-ddTHH:mm:ss.FFF}-{counter:D4}";

        return reference;
    }
}
