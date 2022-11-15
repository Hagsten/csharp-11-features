using Xunit;

namespace Demo.ListPatterns
{
    //Cred: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching
    public class CsvParserDemo
    {
        private const string Csv = """
                    04-01-2020; DEPOSIT;    Initial deposit;            2250,00
                    04-15-2020; DEPOSIT;    Refund;                      125,65
                    04-18-2020; DEPOSIT;    Paycheck;                    825,65
                    04-22-2020; WITHDRAWAL; Debit;           Groceries;  255,73
                    05-01-2020; WITHDRAWAL; #1102;           Rent; apt; 2100,00
                    05-02-2020; INTEREST;                                  0,65
                    05-07-2020; WITHDRAWAL; Debit;           Movies;      12,57
                    04-15-2020; FEE;                                       5,55
                    """;
        //828,10

        public static decimal CalculateBalance()
        {
            return GetTransactions()
                .Sum(transaction => transaction switch
                {
                    [_, "DEPOSIT", _, var amount] => decimal.Parse(amount),
                    [_, "WITHDRAWAL", .., var amount] => -decimal.Parse(amount),
                    [_, "INTEREST", var amount] => decimal.Parse(amount),
                    [_, "FEE", var fee] => -decimal.Parse(fee),
                    _ => throw new InvalidOperationException($"Record {string.Join(", ", transaction)} is not in the expected format!"),
                });
        }

        private static IEnumerable<string[]> GetTransactions()
        {
            return Csv.Split(Environment.NewLine)
                .Select(x => x.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        }

        [Fact]
        public void RunIt()
        {
            var balance = CalculateBalance();

            Assert.True(balance == 828.1M);
        }
    }

    
}
