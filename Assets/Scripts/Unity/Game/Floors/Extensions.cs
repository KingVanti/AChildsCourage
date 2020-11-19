using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    internal static class Extensions
    {
        internal static IEnumerable<RoomPassages> GetPassages(this RoomAsset asset)
        {
            return
                asset
                .GetBasePassages()
                .GetVariations();
        }

        private static RoomPassages GetBasePassages(this RoomAsset asset)
        {
            return new RoomPassages(asset.Id, asset.Room.Passages, 0, false, asset.Room.Type);
        }

        private static IEnumerable<RoomPassages> GetVariations(this RoomPassages room)
        {
            for (var _ = 0; _ < 4; _++)
            {
                yield return room;
                yield return room.Mirror();

                room = room.Rotate();
            }
        }

        private static RoomPassages Mirror(this RoomPassages passages)
        {
            return new RoomPassages(passages.RoomId, passages.Passages.YMirrored, passages.RotationCount, true, passages.Type);
        }

        private static RoomPassages Rotate(this RoomPassages passages)
        {
            return new RoomPassages(passages.RoomId, passages.Passages.Rotated, passages.RotationCount + 1, passages.IsMirrored, passages.Type);
        }


    }

}