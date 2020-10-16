using AChildsCourage.Game.Floors.Persistance;
using System.Collections.Generic;
using UnityEditor;

namespace AChildsCourage.Game.Floors.Editor
{

    public class RoomEditorViewModel
    {

        #region Fields

        private TileType[,] tileTypes = new TileType[Room.MaxSize, Room.MaxSize];
        private RoomAsset _selectedRoomAsset;

        #endregion

        #region Properties

        public RoomAsset SelectedRoomAsset
        {
            get { return _selectedRoomAsset; }
            set
            {
                if (_selectedRoomAsset != value)
                {
                    _selectedRoomAsset = value;
                    OnRoomAssetChanged(value);
                }
            }
        }

        #endregion

        #region Methods

        public void SaveChanges()
        {
            SelectedRoomAsset.RoomShape = new RoomShape(
                new TilePositions(GetPositionsWith(TileType.Wall)));

            SelectedRoomAsset.RoomEntities = new RoomEntities(
                new TilePositions(GetPositionsWith(TileType.Item)),
                new TilePositions(GetPositionsWith(TileType.CourageSmall)),
                new TilePositions(GetPositionsWith(TileType.CourageBig)));

            AssetDatabase.SaveAssets();
        }

        private IEnumerable<TilePosition> GetPositionsWith(TileType tileType)
        {
            for (var x = 0; x < Room.MaxSize; x++)
                for (var y = 0; y < Room.MaxSize; y++)
                    if (tileTypes[x, y] == tileType)
                        yield return new TilePosition(x, y);
        }


        public TileType GetTileTypeAt(int x, int y)
        {
            return tileTypes[x, y];
        }


        public void SetTileTypeAt(int x, int y, TileType tileType)
        {
            tileTypes[x, y] = tileType;
        }


        private void OnRoomAssetChanged(RoomAsset roomAsset)
        {
            if (roomAsset != null)
                LoadTileTypes(roomAsset);
            else
                ClearTileTypes();
        }

        private void ClearTileTypes()
        {
            for (var x = 0; x < Room.MaxSize; x++)
                for (var y = 0; y < Room.MaxSize; y++)
                    tileTypes[x, y] = TileType.Empty;
        }

        private void LoadTileTypes(RoomAsset roomAsset)
        {
            var shape = roomAsset.RoomShape;

            foreach (var wallPosition in shape.WallPositions)
                tileTypes[wallPosition.X, wallPosition.Y] = TileType.Wall;

            var entities = roomAsset.RoomEntities;

            foreach (var itemPositions in entities.ItemPositions)
                tileTypes[itemPositions.X, itemPositions.Y] = TileType.Item;

            foreach (var smallCouragePosition in entities.SmallCouragePositions)
                tileTypes[smallCouragePosition.X, smallCouragePosition.Y] = TileType.CourageSmall;

            foreach (var bigCouragePosition in entities.BigCouragePositions)
                tileTypes[bigCouragePosition.X, bigCouragePosition.Y] = TileType.CourageBig;
        }

        #endregion

    }

}