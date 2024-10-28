namespace ProductImporter.Logic.Transformations.Util
{
    public class ReferenceGenerator : IReferenceGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IInternalCounter _counter;
        private readonly string _prefix;

        public ReferenceGenerator(
            IDateTimeProvider dateTimeProvider,
            IInternalCounter counter,
            string prefix)
        {
            _dateTimeProvider = dateTimeProvider;
            _counter = counter;
            _prefix = prefix;
        }

        public string GetReference()
        {
            var dateTime = _dateTimeProvider.GetUtcDateTime();
            var counter = _counter.GetNextValue();

            var reference = $"{_prefix}-{dateTime:yyyy-MM-ddTHH:mm:ss.FFF}-{counter:D4}";

            return reference;
        }
    }
}
