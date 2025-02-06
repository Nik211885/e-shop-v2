using FluentValidation;

namespace Application.Services.Test.Command.CreateTest
{
    public class CreateTestCommandValidation : AbstractValidator<CreateTestCommand>
    {
        public CreateTestCommandValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name test is required");
            RuleFor(x=>x.Level).NotEmpty().WithMessage("Level test is required");
            RuleFor(x=>x.Type).NotEmpty().WithMessage("Type test is required");
        }
    }
}