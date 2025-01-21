using Core.Entities.Test;
using MediatR;

namespace Application.Services.Test.Query
{
    public record GetListModel : IRequest<List<EntityBoundTest>>;
    public class GetListTest : IRequestHandler<GetListModel, List<EntityBoundTest>>
    {
        public Task<List<EntityBoundTest>> Handle(GetListModel request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}