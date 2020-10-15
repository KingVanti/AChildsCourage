using AChildsCourage.Game.FloorGeneration.Persistance;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.Game.FloorGeneration.Editor
{

    public class RoomEditor
    {

        #region Fields

        private Tilemap _entitiesTileMap;
        private Tilemap _staticTileMap;
        private Tilemap _floorTileMap;
        private Tile _itemTile;
        private Tile _wallTile;
        private Tile _floorTile;

        #endregion

        #region Properties

        private Tilemap EntitiesTileMap
        {
            get
            {
                if (_entitiesTileMap == null)
                    _entitiesTileMap = GameObject.Find("Entities").GetComponent<Tilemap>();
                return _entitiesTileMap;
            }
        }

        private Tilemap StaticTileMap
        {
            get
            {
                if (_staticTileMap == null)
                    _staticTileMap = GameObject.Find("Static").GetComponent<Tilemap>();
                return _staticTileMap;
            }
        }

        private Tilemap FloorTileMap
        {
            get
            {
                if (_floorTileMap == null)
                    _floorTileMap = GameObject.Find("Floor").GetComponent<Tilemap>();
                return _floorTileMap;
            }
        }

        private Tile ItemTile
        {
            get
            {
                if (_itemTile == null)
                    _itemTile = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Sprites/Editor/Tiles/Item.asset");

                return _itemTile;
            }
        }

        private Tile WallTile
        {
            get
            {
                if (_wallTile == null)
                    _wallTile = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Sprites/Editor/Tiles/Wall.asset");

                return _wallTile;
            }
        }

        private Tile FloorTile
        {
            get
            {
                if (_floorTile == null)
                    _floorTile = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Sprites/Editor/Tiles/Floor.asset");

                return _floorTile;
            }
        }

        #endregion

        #region Methods

        public void LoadFromAsset(RoomAsset asset)
        {
            LoadRoomShape(asset.RoomShape);
            LoadRoomEntities(asset.RoomEntities);
        }

        private void LoadRoomShape(RoomShape roomShape)
        {
            WritePositionsToTileMap(roomShape.WallPositions, StaticTileMap, WallTile);
            WritePositionsToTileMap(roomShape.FloorPositions, FloorTileMap, FloorTile);
        }

        private void LoadRoomEntities(RoomEntities roomItems)
        {
            WritePositionsToTileMap(roomItems.ItemPositions, EntitiesTileMap, ItemTile);
        }


        public void SaveChangesToAsset(RoomAsset asset)
        {
            asset.RoomShape = ReadRoomShape();
            asset.RoomEntities = ReadRoomItems();

            EditorUtility.SetDirty(asset);
            Debug.Log("Changes applied to asset. Press Ctrl+S to save!");
        }

        private RoomShape ReadRoomShape()
        {
            var wallPositions = new TilePositions(GetOccupiedPositions(StaticTileMap, "Wall"));
            var floorPositions = new TilePositions(GetOccupiedPositions(FloorTileMap, "Floor"));

            return new RoomShape(wallPositions, floorPositions);
        }

        private RoomEntities ReadRoomItems()
        {
            var itemPositions = new TilePositions(GetOccupiedPositions(EntitiesTileMap, "Item"));

            return new RoomEntities(itemPositions);
        }


        private void WritePositionsToTileMap(TilePositions positions, Tilemap tilemap, Tile tile)
        {
            tilemap.ClearAllTiles();

            foreach (var position in positions)
            {
                var vectorPosition = new Vector3Int(position.X, position.Y, 0);
                tilemap.SetTile(vectorPosition, tile);
            }
        }


        private IEnumerable<TilePosition> GetOccupiedPositions(Tilemap tilemap, string tileName)
        {
            var bounds = tilemap.cellBounds;

            for (var x = bounds.xMin; x <= bounds.xMax; x++)
                for (var y = bounds.yMin; y <= bounds.yMax; y++)
                {
                    var tile = tilemap.GetTile(new Vector3Int(x, y, 0));

                    if (tile != null && tile.name == tileName)
                        yield return new TilePosition(x, y);
                }
        }

        #endregion

    }

}
