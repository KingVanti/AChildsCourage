using System.Collections.Generic;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.Floors.ChunkPassages;

namespace AChildsCourage.Game.Floors.Gen
{

    internal readonly struct RoomConfiguration
    {

        internal static IEnumerable<RoomConfiguration> GetConfigurations(RoomData roomData)
        {
            var baseConfiguration = new RoomConfiguration(roomData.Id, roomData.Type, roomData.Passages, 0, false);

            RoomConfiguration Mirror(RoomConfiguration config) =>
                new RoomConfiguration(config.RoomId, config.roomType, config.passages.Map(MirrorOverXAxis), config.RotationCount, true);

            RoomConfiguration Rotate(RoomConfiguration config) =>
                new RoomConfiguration(config.RoomId, config.roomType, config.passages.Map(ChunkPassages.Rotate), config.RotationCount + 1, config.IsMirrored);

            IEnumerable<RoomConfiguration> GetVariations(RoomConfiguration configuration)
            {
                for (var _ = 0; _ < 4; _++)
                {
                    yield return configuration;
                    yield return Mirror(configuration);
                    configuration = Rotate(configuration);
                }
            }

            return baseConfiguration
                .Map(GetVariations);
        }

        internal static bool MatchesFilter(RoomFilter filter, RoomConfiguration config)
        {
            bool RoomTypesMatch() =>
                config.roomType == filter.RoomType;

            bool PassagesMatch() =>
                filter.Passages.Equals(config.passages);

            return RoomTypesMatch() &&
                   PassagesMatch();
        }


        private readonly RoomType roomType;
        private readonly ChunkPassages passages;


        internal RoomId RoomId { get; }

        internal int RotationCount { get; }

        internal bool IsMirrored { get; }


        private RoomConfiguration(RoomId roomId, RoomType roomType, ChunkPassages passages, int rotationCount, bool isMirrored)
        {
            RoomId = roomId;
            this.roomType = roomType;
            this.passages = passages;
            RotationCount = rotationCount;
            IsMirrored = isMirrored;
        }

    }

}