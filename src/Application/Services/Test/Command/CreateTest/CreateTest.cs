using Core.Entities.Test;
using Core.Interfaces;
using MediatR;

namespace Application.Services.Test.Command.CreateTest
{
    public record CreateTestCommand(string Name) : IRequest<EntityBoundTest>;

    public class CreateTestCommandHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateTestCommand, EntityBoundTest>
    {
        public async Task<EntityBoundTest> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = await unitOfWork.TestBoundRepository.AddAsync(new EntityBoundTest());
            return test;
        }
    }
}