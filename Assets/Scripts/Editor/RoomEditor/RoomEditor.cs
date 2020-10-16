using AChildsCourage.Game.Floors.Persistance;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.Game.Floors.Editor
{

    public class RoomEditor
    {

        #region Constants

        private const string WallTileName = "Wall";
        private const string GroundTileName = "Ground";
        private const string ItemTileName = "Item";
        private const string SmallCourageTileName = "Courage_Small";
        private const string BigCourageTileName = "Courage_Big";
        private const string EntitiesTilemapName = "Entities";
        private const string StaticTilemapName = "Static";
        private const string GroundTilemapName = "Ground";

        #endregion

        #region Fields

        private readonly Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
        private readonly Dictionary<string, Tile> tiles = new Dictionary<string, Tile>();

        #endregion

        #region Methods

        public void LoadFromAsset(RoomAsset asset)
        {
            foreach (var tilemap in tilemaps.Values)
                tilemap.ClearAllTiles();

            LoadRoomShape(asset.RoomShape);
            LoadRoomEntities(asset.RoomEntities);
        }

        private void LoadRoomShape(RoomShape roomShape)
        {
            WritePositionsToTileMap(roomShape.WallPositions, GetTileMap(StaticTilemapName), GetTile(WallTileName));
            WritePositionsToTileMap(roomShape.GroundPositions, GetTileMap(GroundTilemapName), GetTile(GroundTileName));
        }

        private void LoadRoomEntities(RoomEntities roomItems)
        {
            var entitiesMap = GetTileMap(EntitiesTilemapName);

            WritePositionsToTileMap(roomItems.ItemPositions, entitiesMap, GetTile(ItemTileName));
            WritePositionsToTileMap(roomItems.SmallCouragePositions, entitiesMap, GetTile(SmallCourageTileName));
            WritePositionsToTileMap(roomItems.BigCouragePositions, entitiesMap, GetTile(BigCourageTileName));
        }


        public void SaveChangesToAsset(RoomAsset asset)
        {
            asset.RoomShape = ReadRoomShape();
            asset.RoomEntities = ReadRoomEntities();

            EditorUtility.SetDirty(asset);
            Debug.Log("Changes applied to asset. Exit play-mode and press Ctrl+S to save!");
        }

        private RoomShape ReadRoomShape()
        {
            var wallPositions = new TilePositions(GetOccupiedPositions(GetTileMap(StaticTilemapName), WallTileName));
            var groundPositions = new TilePositions(GetOccupiedPositions(GetTileMap(GroundTilemapName), GroundTileName));

            return new RoomShape(wallPositions, groundPositions);
        }

        private RoomEntities ReadRoomEntities()
        {
            var entitiesTilemap = GetTileMap(EntitiesTilemapName);

            var itemPositions = new TilePositions(GetOccupiedPositions(entitiesTilemap, ItemTileName));
            var smallPositions = new TilePositions(GetOccupiedPositions(entitiesTilemap, SmallCourageTileName));
            var bigPositions = new TilePositions(GetOccupiedPositions(entitiesTilemap, BigCourageTileName));

            return new RoomEntities(itemPositions, smallPositions, bigPositions);
        }


        private void WritePositionsToTileMap(TilePositions positions, Tilemap tilemap, Tile tile)
        {
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


        private Tilemap GetTileMap(string name)
        {
            if (!tilemaps.ContainsKey(name))
                tilemaps.Add(name, GameObject.Find(name).GetComponent<Tilemap>());

            return tilemaps[name];
        }

        private Tile GetTile(string name)
        {
            if (!tiles.ContainsKey(name))
                tiles.Add(name, AssetDatabase.LoadAssetAtPath<Tile>($"Assets/Sprites/Editor/Tiles/{name}.asset"));

            return tiles[name];
        }

        #endregion

    }

}
