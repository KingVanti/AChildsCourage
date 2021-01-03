using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct RuneData
    {

        public static RuneData ApplyTo(RuneData _, TilePosition position) =>
            new RuneData(position);

        
        public TilePosition Position { get; }


        public RuneData(TilePosition position) => 
            Position = position;

    }

}