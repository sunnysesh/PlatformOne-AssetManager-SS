using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Profiles;
using PlatformOneAsset.Core.Repositories;
using PlatformOneAsset.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

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

app.UseHttpsRedirection();

app.Run();
