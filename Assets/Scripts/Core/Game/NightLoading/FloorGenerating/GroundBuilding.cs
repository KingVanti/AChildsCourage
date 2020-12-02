﻿using System;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistance;
using static AChildsCourage.F;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorGenerating
    {

        internal static FloorInProgress BuildGroundTiles(TransformTile transformer, GroundTileData[] tiles, FloorInProgress floor)
        {
            Func<GroundTileData, GroundTileData> transformed = tile => TransformGroundTile(tile, transformer);
            Action<GroundTileData> place = tile => PlaceGroundTile(tile, floor);

            Take(tiles)
                .Select(transformed)
                .ForEach(place);

            return floor;
        }

        internal static GroundTileData TransformGroundTile(GroundTileData groundTile, TransformTile transformer)
        {
            var newPosition = transformer(groundTile.Position);

            return groundTile.With(newPosition);
        }

        internal static GroundTileData With(this GroundTileData groundTile, TilePosition position) => new GroundTileData(position, groundTile.DistanceToWall, groundTile.AOIIndex);

        internal static void PlaceGroundTile(GroundTileData groundTile, FloorInProgress floor)
        {
            floor.GroundPositions.Add(groundTile.Position);
        }

    }

}