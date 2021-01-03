using System.Linq;
using static AChildsCourage.Game.Floors.FloorObject;
using static AChildsCourage.Game.ChunkPosition;
using static AChildsCourage.Game.TilePosition;
using static AChildsCourage.Game.TileOffset;

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

        public static RoomContent TransformContent(RoomContent content, ChunkTransform transform)
        {
            FloorObject Transform(FloorObject floorObject) =>
                floorObject
                    .Map(MoveTo, transform.Map(TransformPosition, floorObject.Position));

            return content.Objects
                          .Select(Transform)
                          .Map(RoomContent.Create);
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