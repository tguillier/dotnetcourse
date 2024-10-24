namespace ProductImporter.Model;

public class Money
{
    public const string USD = "USD";
    public const string EUR = "EUR";
    public const decimal USDToEURRate = 0.9m;

    public Money(string isoCurrency, decimal amount)
    {
        IsoCurrency = isoCurrency;
        Amount = amount;
    }
    public string IsoCurrency { get; set; }
    public decimal Amount { get; set; }
}