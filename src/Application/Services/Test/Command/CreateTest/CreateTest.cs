using Application.Helper;
using Application.Interface;
using Core.Entities.Test;
using Core.Events;
using Core.Interfaces;
using MediatR;

namespace Application.Services.Test.Command.CreateTest
{
    public record CreateTestCommand(string Name, string Type, TestLevel Level) : IRequest<EntityBoundTest>;

    public class CreateTestCommandHandler(IUnitOfWork unitOfWork, IFactoryMethod<IPayment> paymentFactoryMethod) 
        : IRequestHandler<CreateTestCommand, EntityBoundTest>
    {
        public async Task<EntityBoundTest> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var testCase = MappingHelper.Mapping<CreateTestCommand, EntityBoundTest>(request);
            var payment = paymentFactoryMethod.Create("Zalo");
            await payment.PayAsync();
            testCase.RaiseEvent(new CreatedNewTestEvent(testCase.Id, testCase.Name));
            await unitOfWork.TestBoundRepository.AddAsync(testCase);
            await unitOfWork.SaveChangeAsync();
            return testCase;
        }
    }
}