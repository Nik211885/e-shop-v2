using FluentValidation;

namespace Application.Services.Test.Command.CreateTest
{
    public class CreateTestCommandValidation : AbstractValidator<CreateTestCommand>
    {
        public CreateTestCommandValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name test is required");
        }
    }
}