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

        //keep track of last placed tile for victory checks
        private Vector2Int lastPlacedTile;

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
            //place tile logic
            switch (direction) {
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
            //store last placed tile
            lastPlacedTile = gridPos;
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
            //store last placed Tile
            lastPlacedTile = placePos;
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

        //============= Victory Check =================
        /// <summary>
        /// returns the longest sequence of tiles that belong to player with playerID.
        /// Only checks around last placed tile!
        /// </summary>
        /// <param name="playerID">ID of the player who to check for</param>
        /// <returns></returns>
        public int FindLongestSequence(int playerID)
        {
            int[] longestSequences = new int[] {
                FindLongestSequenceInDirectionPair(playerID, GridDirection.Up, GridDirection.Down),
                FindLongestSequenceInDirectionPair(playerID, GridDirection.LeftUp, GridDirection.RightDown),
                FindLongestSequenceInDirectionPair(playerID, GridDirection.RightUp, GridDirection.LeftDown)
            };
            return FindHighest(longestSequences);
        }

        private int FindHighest(int[] array)
        {
            int highest = array[0]; //assume array has at least 1 element
            for (int i = 1; i < array.Length; i++) {
                if (array[i] > highest) { highest = array[i]; }
            }
            Debug.Log(highest); //DEL ME
            return highest;
        }

        //============= Find Longest Sequences ================
        /// <summary>
        /// searches grid for longest player owned sequence along 2 given directions, starting from last placed tile
        /// </summary>
        /// <param name="playerID">player id to look for</param>
        /// <param name="upDirection">1st direction to search</param>
        /// <param name="downDirection">2nd direction to search</param>
        /// <returns></returns>
        private int FindLongestSequenceInDirectionPair(int playerID, GridDirection upDirection, GridDirection downDirection)
        {
            //define dirs pair
            GridDirection[] dirs = new GridDirection[] { upDirection, downDirection };
            int sequenceLength = 1; //count last placed tile
            //search in dir pair
            for (int i = 0; i < dirs.Length; i++) {
                Vector2Int searchPos = lastPlacedTile + DirToVector(dirs[i], lastPlacedTile); //start with offset in direction to search
                while (PosIsInOwnedBounds(searchPos) && IsPlayerOwnedPos(searchPos, playerID)) {
                    //add to sequence
                    sequenceLength++;
                    searchPos += DirToVector(dirs[i], searchPos); //continue search
                }
            }
            //return result
            return sequenceLength;
        }

        private bool PosIsInOwnedBounds(Vector2Int pos)
        {
            return pos.x >= 0 && pos.x < gridData.Count && //x bounds check
                pos.y >= 0 && pos.y < gridData[pos.x].Count; //y bounds check
        }

        private Vector2Int DirToVector(GridDirection dir, Vector2Int currentPos)
        {
            return GridDirectionUtil.GridDirectionToVector2(dir, gridVisuals[currentPos.x][currentPos.y], this);
        }

        private bool IsPlayerOwnedPos(Vector2Int pos, int playerID) {
            return gridData[pos.x][pos.y] == playerID;
        }
    }
}
