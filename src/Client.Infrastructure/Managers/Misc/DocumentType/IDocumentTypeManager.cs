using System.Collections.Generic;
using System.Threading.Tasks;
using Repres.Application.Features.DocumentTypes.Commands.AddEdit;
using Repres.Application.Features.DocumentTypes.Queries.GetAll;
using Repres.Shared.Wrapper;

namespace Repres.Client.Infrastructure.Managers.Misc.DocumentType
{
    public interface IDocumentTypeManager : IManager
    {
        Task<IResult<List<GetAllDocumentTypesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDocumentTypeCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}