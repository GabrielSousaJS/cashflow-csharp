using System.Reflection.Metadata.Ecma335;

namespace CashFlow.Communication.Responses;

public class ResponseShortExpenseJson
{
    public long Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
}
