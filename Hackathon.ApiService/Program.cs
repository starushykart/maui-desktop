using Hackathon.ApiService.Common.HostedServices;
using Hackathon.ApiService.Common.Mediatr;
using Hackathon.ApiService.Features.GetDocumentById;
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
    var id = await mediator.Send(new UploadDocumentCommand(file));
    return Results.Created($"/documents/{id}", null);
}).DisableAntiforgery();

app.MapGet("/documents/{documentId:guid}", async (IMediator mediator, Guid documentId) =>
{
    var document = await mediator.Send(new GetDocumentByIdRequest(documentId));
    return document == null ? Results.NotFound() : Results.Ok(document);
});

app.MapDefaultEndpoints();
app.MapHub<DocumentsNotificationHub>("/documentsHub");

app.Run();
