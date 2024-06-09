using FluentValidation;
using PlatformOneAsset.Core.Exceptions;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Profiles;
using PlatformOneAsset.Core.Repositories;
using PlatformOneAsset.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddSingleton<IAssetRepository, AssetRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/assets", async (IAssetService assetService) =>
{
    var assetResponses = await assetService.GetAllAssetsAsync();
    return Results.Ok(assetResponses);
});

app.MapGet("/assets/{symbol}", async (string symbol, IAssetService assetService) =>
{
    var response = await assetService.GetAssetViaSymbolAsync(symbol);
    if (response == null)
        return Results.NotFound();

    return Results.Ok(response);
});

app.MapPost("/assets", async (CreateAssetRequest request ,IAssetService assetService, IValidator<CreateAssetRequest> validator) =>
{
    var validResult = await validator.ValidateAsync(request);
    if (!validResult.IsValid)
        return Results.BadRequest($"Validation error occured. {validResult.ToString()}");
    
    try
    {
        var response = await assetService.AddAssetAsync(request);
        return Results.Created($"/assets/{response.Symbol}", response);
    }
    catch (EntityAlreadyExistsException ex)
    {
        return Results.Conflict(new
        {
            message = ex.Message
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            statusCode: StatusCodes.Status500InternalServerError
        );
    }
});

app.MapPut("/assets/{symbol}", async (UpdateAssetRequest request, string symbol, IAssetService assetService,
    IValidator<UpdateAssetRequest> validator) =>
{
    if (string.IsNullOrEmpty(symbol))
        return Results.BadRequest("Validation error occured. Please provide a symbol");
    
    var validResult = await validator.ValidateAsync(request);
    if (!validResult.IsValid)
        return Results.BadRequest($"Validation error occured. {validResult.ToString()}");

    try
    {
        var result = await assetService.UpdateAssetAsync(symbol, request);
        return Results.Ok(result);
    }
    catch (EntityNotFoundException ex)
    {
        return Results.NotFound(new
        {
            message = ex.Message
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            statusCode: StatusCodes.Status500InternalServerError
        );
    }
});

app.UseHttpsRedirection();

app.Run();
