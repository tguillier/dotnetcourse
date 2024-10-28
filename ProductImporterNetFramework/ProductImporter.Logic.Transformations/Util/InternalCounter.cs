namespace ProductImporter.Logic.Transformations.Util
{
    public class InternalCounter : IInternalCounter
    {
        private int _counter = -1;

        public int GetNextValue()
        {
            _counter++;
            return _counter;
        }
    }
}
