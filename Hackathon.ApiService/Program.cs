using Hackathon.ApiService.Common.HostedServices;
using Hackathon.ApiService.Common.Mediatr;
using Hackathon.ApiService.Features.DeleteDocumentById;
using Hackathon.ApiService.Features.DownloadDocument;
using Hackathon.ApiService.Features.GetAllDocuments;
using Hackathon.ApiService.Features.GetDocumentById;
using Hackathon.ApiService.Features.PatchDocumentById;
using Hackathon.ApiService.Features.UploadDocument;
using Hackathon.ApiService.Persistence;
using Hackathon.ApiService.Services.FileStorage;
using Hackathon.ApiService.Services.SignalR;
using Hackathon.ServiceDefaults;
using LocalStack.Client.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddMediatr();
builder.Services.AddLocalStack(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddFileStorage();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IActionsReporter, HubActionsReporter>();

builder.AddHostedServices();
builder.AddNpgsqlDbContext<ApplicationDbContext>("hackathonDb");

var app = builder.Build();

app.UseExceptionHandler();

app.MapPost("/documents", async (IMediator mediator, IFormFile file) =>
{
    var document = await mediator.Send(new UploadDocumentCommand(file));
    return Results.Created($"/documents/{document.Id}", document);
}).DisableAntiforgery();

app.MapGet("/documents/{documentId:guid}", async (IMediator mediator, Guid documentId) =>
{
    var document = await mediator.Send(new GetDocumentByIdRequest(documentId));
    return document == null ? Results.NotFound() : Results.Ok(document);
});

app.MapGet("/documents", async (IMediator mediator) =>
{
    var documents = await mediator.Send(new GetAllDocumentsRequest());
    return Results.Ok(documents);
});

app.MapDelete("/documents/{documentId:guid}", async (IMediator mediator, Guid documentId) =>
{
    await mediator.Send(new DeleteDocumentByIdRequest(documentId));
    return Results.NoContent();
});

app.MapPatch("/documents/{documentId:guid}", async (IMediator mediator, Guid documentId, PatchDocumentByIdRequest request) =>
{
    await mediator.Send(request with { Id = documentId });
    return Results.NoContent();
});

app.MapGet("/documents/{documentId:guid}/content", async (IMediator mediator, Guid documentId) =>
{
    var response = await mediator.Send(new DownloadDocumentCommand(documentId));
    return Results.File(response.Stream);
});


app.MapGet("/ping", () => Task.FromResult(Results.Ok()));

app.MapDefaultEndpoints();
app.MapHub<DocumentsNotificationHub>("/documentsHub");

app.Run();
