namespace ProductImporter.Util;

public class InternalCounter : IInternalCounter
{
    private int counter = -1;

    public int GetNextValue()
    {
        counter++;
        return counter;
    }
}
