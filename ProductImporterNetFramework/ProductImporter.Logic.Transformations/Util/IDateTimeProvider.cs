using System;

namespace ProductImporter.Logic.Transformations.Util
{
    public interface IDateTimeProvider
    {
        DateTime GetUtcDateTime();
    }
}