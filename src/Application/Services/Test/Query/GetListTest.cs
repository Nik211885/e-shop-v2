using Core.Entities.Test;
using Core.Interfaces.Repository.Test;
using MediatR;

namespace Application.Services.Test.Query
{
    public record GetListModel : IRequest<IReadOnlyCollection<EntityBoundTest>>;
    public class GetListTest(ITestBoundRepository testBoundRepository) 
        : IRequestHandler<GetListModel, IReadOnlyCollection<EntityBoundTest>>
    {
        public Task<IReadOnlyCollection<EntityBoundTest>> Handle(GetListModel request, CancellationToken cancellationToken)
        {
            var listTestCase = testBoundRepository.GetAllAsync();
            return listTestCase;
        }
    }
}