namespace ProductImporter.Logic.Transformations.Util
{
    public interface IReferenceGeneratorFactory
    {
        IReferenceGenerator Create(string prefix);
    }
}
