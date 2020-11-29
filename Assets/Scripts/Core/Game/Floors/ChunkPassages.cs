using System;
using Newtonsoft.Json;

namespace AChildsCourage.Game.Floors
{

    public readonly struct ChunkPassages
    {

        #region Static Properties

        public static ChunkPassages None => new ChunkPassages(false, false, false, false);

        public static ChunkPassages All => new ChunkPassages(true, true, true, true);

        #endregion

        #region Properties

        public bool HasNorth { get; }

        public bool HasEast { get; }

        public bool HasSouth { get; }

        public bool HasWest { get; }


        [JsonIgnore] public int Count => (HasNorth ? 1 : 0) + (HasEast ? 1 : 0) + (HasSouth ? 1 : 0) + (HasWest ? 1 : 0);


        [JsonIgnore] public ChunkPassages Rotated => new ChunkPassages(HasWest, HasNorth, HasEast, HasSouth);


        [JsonIgnore] public ChunkPassages YMirrored => new ChunkPassages(HasSouth, HasEast, HasNorth, HasWest);

        #endregion

        #region Constructors

        public ChunkPassages(bool hasNorth, bool hasEast, bool hasSouth, bool hasWest)
        {
            HasNorth = hasNorth;
            HasEast = hasEast;
            HasSouth = hasSouth;
            HasWest = hasWest;
        }

        #endregion

        #region Methods

        public bool Has(PassageDirection passage)
        {
            switch (passage)
            {
                case PassageDirection.North:
                    return HasNorth;
                case PassageDirection.East:
                    return HasEast;
                case PassageDirection.South:
                    return HasSouth;
                case PassageDirection.West:
                    return HasWest;
            }

            throw new Exception("Invalid passage!");
        }


        public override bool Equals(object obj)
        {
            return obj is ChunkPassages passages &&
                   HasNorth == passages.HasNorth &&
                   HasEast == passages.HasEast &&
                   HasSouth == passages.HasSouth &&
                   HasWest == passages.HasWest;
        }


        public override int GetHashCode()
        {
            var hashCode = 1909415112;
            hashCode = hashCode * -1521134295 + HasNorth.GetHashCode();
            hashCode = hashCode * -1521134295 + HasEast.GetHashCode();
            hashCode = hashCode * -1521134295 + HasSouth.GetHashCode();
            hashCode = hashCode * -1521134295 + HasWest.GetHashCode();
            return hashCode;
        }

        #endregion

    }

}