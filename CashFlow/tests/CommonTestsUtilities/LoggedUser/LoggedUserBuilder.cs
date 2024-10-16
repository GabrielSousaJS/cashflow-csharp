using CashFlow.Domain.Entities;
using CashFlow.Domain.Services.LoogedUser;
using Moq;

namespace CommonTestsUtilities.LoggedUser;

public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        return mock.Object;
    }
}
