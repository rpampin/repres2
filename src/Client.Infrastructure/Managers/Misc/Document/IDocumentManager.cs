using Repres.Application.Features.Documents.Commands.AddEdit;
using Repres.Application.Features.Documents.Queries.GetAll;
using Repres.Application.Requests.Documents;
using Repres.Shared.Wrapper;
using System.Threading.Tasks;
using Repres.Application.Features.Documents.Queries.GetById;

namespace Repres.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}