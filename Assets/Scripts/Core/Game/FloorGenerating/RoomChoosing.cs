using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class MFloorGenerating
    {

        private static IEnumerable<TransformedRoomData> ChooseRoomsFor(FloorPlan floorPlan, IEnumerable<RoomData> rooms)
        {
            var roomsArray = rooms.ToArray();

            foreach (var roomPlan in floorPlan.Rooms)
            {
                var roomData = roomsArray.First(r => r.Id == roomPlan.RoomId);
                var roomContent = roomData.Content;
                var transformed = TransformContent(roomContent, roomPlan.Transform, roomData.Type);

                yield return transformed;
            }
        }

        private static TransformedRoomData TransformContent(RoomContentData content, RoomTransform roomTransform, RoomType roomType)
        {
            var transformer = CreateTransformerFor(roomTransform);

            return new TransformedRoomData(
                content.GroundData.Select(t => TransformGroundTile(t, transformer)).ToImmutableHashSet(),
                content.StaticObjects.Select(o => TransformStaticObject(o, transformer)).ToImmutableHashSet(),
                content.CourageData.Select(c => TransformCouragePickup(c, transformer)).ToImmutableHashSet(),
                roomType,
                roomTransform.Position);
        }

        private static TransformTile CreateTransformerFor(RoomTransform roomTransform)
        {
            var transform = ToChunkTransform(roomTransform);
            return position => Transform(position, transform);
        }

        internal static GroundTileData TransformGroundTile(GroundTileData groundTile, TransformTile transformer) => groundTile.With(transformer(groundTile.Position));

        internal static GroundTileData With(this GroundTileData groundTile, TilePosition position) =>
            new GroundTileData(
                position,
                groundTile.DistanceToWall,
                groundTile.AoiIndex);

        internal static StaticObjectData TransformStaticObject(StaticObjectData staticObject, TransformTile transformer) => staticObject.With(transformer(staticObject.Position));

        internal static StaticObjectData With(this StaticObjectData staticObject, TilePosition position) =>
            new StaticObjectData(
                position);

        internal static CouragePickupData TransformCouragePickup(CouragePickupData pickup, TransformTile transformer) => pickup.With(transformer(pickup.Position));

        internal static CouragePickupData With(this CouragePickupData pickup, TilePosition position) =>
            new CouragePickupData(
                position,
                pickup.Variant);

    }

}