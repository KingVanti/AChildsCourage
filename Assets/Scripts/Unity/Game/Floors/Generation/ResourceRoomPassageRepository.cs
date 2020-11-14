using AChildsCourage.Game.Floors.Persistance;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Generation
{

    internal class ResourceRoomPassageRepository : IRoomPassagesRepository
    {

        #region Constants

        private const string RoomResourcePath = "Rooms/";

        #endregion

        #region Fields

        private readonly RoomPassages[] startRooms;
        private readonly RoomPassages[] normalRooms;
        private readonly RoomPassages[] endRooms;

        #endregion

        #region Constructors

        internal ResourceRoomPassageRepository()
        {
            var allRooms = GetAllPassages();

            startRooms = allRooms.Where(r => r.Type == RoomType.Start).ToArray();
            normalRooms = allRooms.Where(r => r.Type == RoomType.Normal).ToArray();
            endRooms = allRooms.Where(r => r.Type == RoomType.End).ToArray();
        }

        #endregion

        #region Methods

        public FilteredRoomPassages GetStartRooms()
        {
            return new FilteredRoomPassages(startRooms);
        }


        public FilteredRoomPassages GetNormalRooms(ChunkPassageFilter filter, int maxLooseEnds)
        {
            return new FilteredRoomPassages(FilterRoomsFor(filter, maxLooseEnds));
        }

        private IEnumerable<RoomPassages> FilterRoomsFor(ChunkPassageFilter filter, int maxLooseEnds)
        {
            return normalRooms.Where(r => RoomMatchesFilter(r, filter, maxLooseEnds));
        }


        public FilteredRoomPassages GetEndRooms(ChunkPassageFilter filter)
        {
            return new FilteredRoomPassages(endRooms.Where(r => RoomMatchesFilter(r, filter, 0)));
        }


        private IEnumerable<RoomPassages> GetAllPassages()
        {
            var assets = LoadAssets();

            return assets.SelectMany(GetPassages);
        }

        private IEnumerable<RoomAsset> LoadAssets()
        {
            return Resources.LoadAll<RoomAsset>(RoomResourcePath).OrderBy(a => a.Id);
        }

        private IEnumerable<RoomPassages> GetPassages(RoomAsset asset)
        {
            return
                GetPassageVariations(asset.Room.Passages)
                .Distinct()
                .Select(p => new RoomPassages(asset.Id, p, asset.Room.Type));
        }

        private IEnumerable<ChunkPassages> GetPassageVariations(ChunkPassages passages)
        {
            foreach (var passage in GetRotations(passages))
                yield return passage;
            foreach (var passage in GetRotations(passages.XMirrored))
                yield return passage;
        }

        private IEnumerable<ChunkPassages> GetRotations(ChunkPassages passages)
        {
            yield return passages;
            yield return passages.Rotated;
            yield return passages.Rotated.Rotated;
            yield return passages.Rotated.Rotated.Rotated;
        }


        private bool RoomMatchesFilter(RoomPassages roomPassages, ChunkPassageFilter filter, int maxLooseEnds)
        {
            return filter.Matches(roomPassages.Passages) && filter.FindLooseEnds(roomPassages.Passages) <= maxLooseEnds;
        }

        #endregion

    }

}