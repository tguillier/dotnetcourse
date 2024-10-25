namespace ProductImporter.Logic.Transformations.Util;

public class ReferenceGeneratorFactory : IReferenceGeneratorFactory
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IInternalCounter _counter;

    public ReferenceGeneratorFactory(
        IDateTimeProvider dateTimeProvider,
        IInternalCounter counter)
    {
        _dateTimeProvider = dateTimeProvider;
        _counter = counter;
    }

    public IReferenceGenerator Create(string prefix)
    {
        return new ReferenceGenerator(_dateTimeProvider, _counter, prefix);
    }
}
