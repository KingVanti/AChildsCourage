using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.Floors.RoomPersistence.CouragePickupData;
using static AChildsCourage.Game.Floors.RoomPersistence.GroundTileData;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.Floors.RoomPersistence.MRoomContentData;
using static AChildsCourage.Game.Floors.RoomPersistence.RuneData;
using static AChildsCourage.Game.Floors.RoomPersistence.StaticObjectData;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct ChunkTransform
    {

        public static ChunkTransform CreateTransform(RoomInstance room)
        {
            var chunkCenter = room.Position.Map(GetCenter);
            var chunkCorner = room.Position.Map(GetCorner);

            return new ChunkTransform(room.RotationCount, room.IsMirrored, chunkCorner, chunkCenter);
        }

        public static RoomContentData TransformContent(RoomContentData content, ChunkTransform transform)
        {
            TilePosition Transform(TilePosition p) =>
                transform.Map(TransformPosition, p);

            GroundTileData TransformGroundTile(GroundTileData groundTile) =>
                groundTile.Position
                          .Map(Transform)
                          .Map(ApplyTo, groundTile);

            StaticObjectData TransformStaticObject(StaticObjectData staticObject) =>
                staticObject.Position
                            .Map(Transform)
                            .Map(ApplyTo, staticObject);

            RuneData TransformRune(RuneData rune) =>
                rune.Position
                    .Map(Transform)
                    .Map(ApplyTo, rune);

            CouragePickupData TransformCouragePickup(CouragePickupData pickup) =>
                pickup.Position
                      .Map(Transform)
                      .Map(ApplyTo, pickup);

            return new RoomContentData(content.GroundData.Select(TransformGroundTile).ToArray(),
                                       content.CourageData.Select(TransformCouragePickup).ToArray(),
                                       content.StaticObjects.Select(TransformStaticObject).ToArray(),
                                       content.Runes.Select(TransformRune).ToArray());
        }

        private static TilePosition TransformPosition(TilePosition position, ChunkTransform transform)
        {
            TilePosition Translate(TilePosition p) =>
                transform.chunkCorner
                         .Map(AsOffset)
                         .Map(ApplyTo, p);

            TilePosition Rotate(TilePosition p) =>
                p.Map(RotateAround, transform.chunkCenter);

            TilePosition Mirror(TilePosition p) =>
                p.Map(YMirrorOver, transform.chunkCenter);

            return position
                   .Map(Translate)
                   .For(transform.rotationCount, Rotate)
                   .DoIf(Mirror, transform.isMirrored);
        }


        private readonly int rotationCount;

        private readonly bool isMirrored;

        private readonly TilePosition chunkCorner;

        private readonly TilePosition chunkCenter;


        private ChunkTransform(int rotationCount, bool isMirrored, TilePosition chunkCorner, TilePosition chunkCenter)
        {
            this.rotationCount = rotationCount;
            this.isMirrored = isMirrored;
            this.chunkCorner = chunkCorner;
            this.chunkCenter = chunkCenter;
        }

    }

}