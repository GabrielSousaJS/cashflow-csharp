using Bogus;
using CashFlow.Communication.Requests;

namespace CommonTestsUtilities.Requests;

public class RequestChangePasswordJsonBuilder
{
    public static RequestChangePasswordJson Build()
    {
        return new Faker<RequestChangePasswordJson>()
            .RuleFor(request => request.Password, faker => faker.Internet.Password())
            .RuleFor(request => request.NewPassword, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}
