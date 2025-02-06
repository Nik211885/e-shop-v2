using Application.Helper;
using Core.Entities.Test;
using Core.Events;
using Core.Interfaces;
using MediatR;

namespace Application.Services.Test.Command.CreateTest
{
    public record CreateTestCommand(string Name, string Type, TestLevel Level) : IRequest<EntityBoundTest>;

    public class CreateTestCommandHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateTestCommand, EntityBoundTest>
    {
        public async Task<EntityBoundTest> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var testCase = MappingHelper.Mapping<CreateTestCommand, EntityBoundTest>(request);
            testCase.RaiseEvent(new CreatedNewTestEvent());
            await unitOfWork.TestBoundRepository.AddAsync(testCase);
            await unitOfWork.SaveChangeAsync();
            return testCase;
        }
    }
}