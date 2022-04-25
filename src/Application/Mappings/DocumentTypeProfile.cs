using AutoMapper;
using Repres.Application.Features.DocumentTypes.Commands.AddEdit;
using Repres.Application.Features.DocumentTypes.Queries.GetAll;
using Repres.Application.Features.DocumentTypes.Queries.GetById;
using Repres.Domain.Entities.Misc;

namespace Repres.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}