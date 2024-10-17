using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestsUtilities.Requests;

public class RequestExpenseJsonBuilder
{
    public static RegisterExpenseJson Build()
    {
        return new Faker<RegisterExpenseJson>()
            .RuleFor(request => request.Title, faker => faker.Commerce.ProductName())
            .RuleFor(request => request.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(request => request.Date, faker => faker.Date.Past())
            .RuleFor(request => request.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(request => request.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(request => request.Tags, faker => faker.Make(1, () => faker.PickRandom<Tag>()));
    }
}
