using CashFlow.Application.UseCases.Users.ChangePassword;
using CashFlow.Exception;
using CommonTestsUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Users.ChangePassword;

public class ChangePasswordValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Error_NewPassword_Empty(string newPassword)
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = newPassword;

        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(errors => errors.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSOWORD));
    }
}
