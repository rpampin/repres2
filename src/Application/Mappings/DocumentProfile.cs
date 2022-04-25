using AutoMapper;
using Repres.Application.Features.Documents.Commands.AddEdit;
using Repres.Application.Features.Documents.Queries.GetById;
using Repres.Domain.Entities.Misc;

namespace Repres.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}