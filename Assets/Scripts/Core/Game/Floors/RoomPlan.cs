namespace AChildsCourage.Game.Floors
{

    public readonly struct RoomPlan
    {

        #region Properties

        public int RoomId { get; }

        public RoomTransform Transform { get; }

        #endregion

        #region Constructors

        public RoomPlan(int roomId, RoomTransform transform)
        {
            RoomId = roomId;
            Transform = transform;
        }

        #endregion

    }

}