﻿namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public readonly struct RoomId
    {

        private readonly int value;


        private RoomId(int value)
        {
            this.value = value;
        }


        public static explicit operator RoomId(int id) => new RoomId(id);

        public static implicit operator int(RoomId id) => id.value;

    }

}