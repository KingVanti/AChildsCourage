namespace AChildsCourage.Game.Floors
{
    public delegate FloorPlan GenerateFloor(int seed);

    public delegate FloorTiles BuildRoomTiles(FloorPlan floorPlan);

}