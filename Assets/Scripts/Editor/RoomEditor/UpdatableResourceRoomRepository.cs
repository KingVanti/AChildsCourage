namespace AChildsCourage.Game.FloorGeneration.Persistance.Editor
{

    public class UpdatableResourceRoomRepository : ResourceRoomRepository, IUpdatableRoomRepository
    {

        #region Methods

        public void Update(Room room, int id)
        {
            var asset = GetRoomAsset(id);

            asset.RoomShape = room.Shape;
        }

        #endregion

    }

}