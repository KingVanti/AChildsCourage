using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public static class MSerializedRoomContent
    {

        public static SerializedRoomContent NoContent => new SerializedRoomContent(null, null, null, null);

        public static RoomContent ReadContent(SerializedRoomContent content)
        {
            IEnumerable<FloorObject> ReadGroundData() =>
                content.GroundData.Select(g => new FloorObject(g.Position, new GroundTileData()));

            IEnumerable<FloorObject> ReadCouragePickupData() =>
                content.CourageData.Select(c => new FloorObject(c.Position, new CouragePickupData(c.Variant)));

            IEnumerable<FloorObject> ReadStaticObjectData() =>
                content.StaticObjects.Select(s => new FloorObject(s.Position, new StaticObjectData()));

            IEnumerable<FloorObject> ReadRuneData() =>
                content.Runes.Select(r => new FloorObject(r.Position, new RuneData()));

            return ReadGroundData()
                   .Concat(ReadCouragePickupData())
                   .Concat(ReadStaticObjectData())
                   .Concat(ReadRuneData())
                   .Map(RoomContent.Create);
        }

        public class SerializedRoomContent
        {

            public SerializedGroundTile[] GroundData { get; }

            public SerializedCouragePickup[] CourageData { get; }

            public SerializedStaticObject[] StaticObjects { get; }

            public SerializedRune[] Runes { get; }


            public SerializedRoomContent(SerializedGroundTile[] groundData, SerializedCouragePickup[] courageData, SerializedStaticObject[] staticObjects, SerializedRune[] runes)
            {
                GroundData = groundData ?? new SerializedGroundTile[0];
                CourageData = courageData ?? new SerializedCouragePickup[0];
                StaticObjects = staticObjects ?? new SerializedStaticObject[0];
                Runes = runes ?? new SerializedRune[0];
            }

        }

    }

}