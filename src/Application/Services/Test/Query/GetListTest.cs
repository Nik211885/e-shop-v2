using Core.Entities.Test;
using Core.Interfaces;
using MediatR;

namespace Application.Services.Test.Query
{
    public record GetListModel : IRequest<IReadOnlyCollection<EntityBoundTest>>;
    public class GetListTest(IUnitOfWork unitOfWork) 
        : IRequestHandler<GetListModel, IReadOnlyCollection<EntityBoundTest>>
    {
        public async Task<IReadOnlyCollection<EntityBoundTest>> Handle(GetListModel request, CancellationToken cancellationToken)
        {
            var listTestCase = await unitOfWork.TestBoundRepository.GetAllAsync();
            return listTestCase;
        }
    }
}