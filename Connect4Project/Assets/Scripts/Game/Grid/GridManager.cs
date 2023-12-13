using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    /// <summary>
    /// This class updates and handles the grid data
    /// </summary>
    public class GridManager : MonoBehaviour
    {
        //use dependency injection through the editor
        [Header("Refs")]
        [SerializeField] private GridGenerator gridGenerator;

        //vars
        [HideInInspector] public List<List<GridTile>> gridVisuals; //holds data regarding the visuals of the grid
        private List<List<int>> gridData; //only holds data about who owns each grid tile

        private void Start()
        {
            //create grid
            gridVisuals = gridGenerator.GenerateGrid();
            InitializeGridData();
            //frame camera
            EventBus<CameraFrameReqEvent>.Invoke(
                new CameraFrameReqEvent {
                    center = gridGenerator.GetGridCenter(), 
                    bounds = gridGenerator.GetGridBounds() 
                }
            );
        }
        private void InitializeGridData()
        {
            gridData = new List<List<int>>();
            for (int i = 0; i < gridVisuals.Count; i++)
            {
                gridData.Add(new List<int>());
            }
        }

        //========== Util ==========
        public GridTile GetTopTileInColumn(int column)
        {
            return gridVisuals[column][^1];
        }

        public bool TryGetGridTileAtPos(Vector2Int pos, out GridTile tile)
        {
            tile = null;
            if (PosIsInBounds(pos))
            {
                tile = gridVisuals[pos.x][pos.y];
                return true;
            }
            return false;
        }
        private bool PosIsInBounds(Vector2Int pos)
        {
            return pos.x >= 0 && pos.x < gridVisuals.Count &&
                pos.y >= 0 && pos.y < gridVisuals[pos.x].Count;
        }

        //=========== Place Tile =============
        //these funcs are called by the gamestate manager
        public bool CanPlace(int column)
        {
            return gridData[column].Count < gridVisuals[column].Count; //check if first tile in column has space
        }

        public void PlaceTile(int playerID, int column, GridDirection direction)
        {
            switch (direction)
            {
                case GridDirection.Down:
                    PlaceTileDown(playerID, column);
                    break;

                case GridDirection.LeftDown:
                case GridDirection.RightDown:
                    PlaceTileDiagonal(playerID, column, direction);
                    break;

                default: break; //this should never be called
            }
        }

        //====== Tile Placement Logic =======
        private void PlaceTileDown(int playerID, int column)
        {
            //add to column top
            gridData[column].Add(playerID);
            //update visuals
            Vector2Int gridPos = new Vector2Int(column, gridData[column].Count - 1);
            gridVisuals[gridPos.x][gridPos.y].UpdateVisuals(playerID);
        }

        private void PlaceTileDiagonal(int playerID, int column, GridDirection direction)
        {
            Vector2Int placePos = new Vector2Int(column, gridVisuals[column].Count - 1);
            //recursive search place pos
            while (IsValidPosition(placePos + GridDirectionUtil.GridDirectionToVector2(direction, gridVisuals[placePos.x][placePos.y], this)))
            {
                placePos += GridDirectionUtil.GridDirectionToVector2(direction, gridVisuals[placePos.x][placePos.y], this);
            }
            //place tile
            if (gridData[placePos.x].Count > placePos.y) { //not top tile in column, simply set data
                gridData[placePos.x][placePos.y] = playerID;
            }
            else { //top tile, fill blanks beneath
                while (gridData[placePos.x].Count < placePos.y) { gridData[placePos.x].Add(-1); }
                gridData[placePos.x].Add(playerID); //place new tile on top
            }
            //update visuals
            gridVisuals[placePos.x][placePos.y].UpdateVisuals(playerID);
        }

        private bool IsValidPosition(Vector2Int gridPos)
        {
            return PosIsInBounds(gridPos) && !IsTaken(gridPos);
        }

        private bool IsTaken(Vector2Int validGridPos)
        {
            if (gridData[validGridPos.x].Count <= validGridPos.y) { return false; } //check if pos is higher than highest tile in column
            else { return gridData[validGridPos.x][validGridPos.y] >= 0; } //check if pos has been claimed
        }
    }
}
