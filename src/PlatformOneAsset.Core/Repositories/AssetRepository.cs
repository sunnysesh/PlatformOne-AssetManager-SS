﻿using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;

namespace PlatformOneAsset.Core.Repositories;

public class AssetRepository : BaseRepository<Asset>, IAssetRepository
{
    public Asset GetBySymbol(string symbol)
    {
        return _entities.Values.Where(i => i.Symbol == symbol).FirstOrDefault();
    }

    public bool AssetExists(string symbol)
        => _entities.Values.Any(i => i.Symbol == symbol);

    protected override string GetEntityId(Asset entity)
        => entity.Symbol;
}