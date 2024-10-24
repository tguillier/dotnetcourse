using ProductImporter.Model;
using System.Globalization;
using System.Text;

namespace ProductImporter.Logic.Target;

public class ProductFormatter : IProductFormatter
{
    private const string _headerLine = "Id,Name,Currency,Price,Stock";

    public string Format(Product product)
    {
        var stringBuilder = new StringBuilder();

        AppendItem(stringBuilder, product.Id.ToString(), true);
        AppendItem(stringBuilder, product.Name, false);
        AppendItem(stringBuilder, product.Price.IsoCurrency, false);
        AppendItem(stringBuilder, product.Price.Amount.ToString(CultureInfo.InvariantCulture), false);
        AppendItem(stringBuilder, product.Stock.ToString(), false);
        AppendItem(stringBuilder, product.Reference, false);

        return stringBuilder.ToString();
    }

    public string GetHeaderLine()
    {
        return _headerLine;
    }

    private static void AppendItem(StringBuilder stringBuilder, string item, bool isFirst)
    {
        if (!isFirst)
        {
            stringBuilder.Append(',');
        }

        if (item.Any(char.IsWhiteSpace))
        {
            stringBuilder.Append('"');
            stringBuilder.Append(item);
            stringBuilder.Append('"');
        }
        else
        {
            stringBuilder.Append(item);
        }
    }
}