using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        private static IEnumerable<TransformedRoomData> ChooseRoomsFor(FloorPlan floorPlan, IEnumerable<RoomData> rooms)
        {
            var roomsArray = rooms.ToArray();

            foreach (var roomPlan in floorPlan.Rooms)
            {
                var roomContent = roomsArray.First(r => r.Id == roomPlan.RoomId).Content;
                var transformed = TransformContent(roomContent, roomPlan.Transform);

                yield return transformed;
            }
        }

        private static TransformedRoomData TransformContent(RoomContentData content, RoomTransform roomTransform)
        {
            var transformer = CreateTransformerFor(roomTransform);

            return new TransformedRoomData(
                content.GroundData.Select(t => TransformGroundTile(t, transformer)).ToImmutableHashSet(),
                content.CourageData.Select(t => TransformCouragePickup(t, transformer)).ToImmutableHashSet());
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

        internal static CouragePickupData TransformCouragePickup(CouragePickupData pickup, TransformTile transformer) => pickup.With(transformer(pickup.Position));

        internal static CouragePickupData With(this CouragePickupData pickup, TilePosition position) =>
            new CouragePickupData(
                position,
                pickup.Variant);

    }

}