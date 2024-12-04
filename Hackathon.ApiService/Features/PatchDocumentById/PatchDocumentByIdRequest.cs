using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.ApiService.Features.PatchDocumentById;

public record PatchDocumentByIdRequest([FromRoute]Guid Id, string? Name) : IRequest;