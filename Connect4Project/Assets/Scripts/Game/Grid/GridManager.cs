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
        private List<List<GridTile>> grid;
        private List<List<int>> gridData = new List<List<int>>();

        private void Start()
        {
            //create grid
            grid = gridGenerator.GenerateGrid();
            InitializeGridData();
            //frame camera
            EventBus<CameraFrameReqEvent>.Invoke(
                new CameraFrameReqEvent { 
                    teleport = true, 
                    center = gridGenerator.GetGridCenter(), 
                    bounds = gridGenerator.GetGridBounds() 
                }
            );
        }
        private void InitializeGridData()
        {
            for (int i = 0; i < grid.Count; i++)
            {
                gridData.Add(new List<int>());
            }
        }

        //=========== Place Tile =============
        //public bool CanPlace(int column)
        //{
        //return grid[column, grid.GetLength(1) - 1].ownerID == -1; //check if there is at least 1 space left in column
        //}

        public void PlaceTile(int playerID, int column)
        {
            //add to data
            //gridData[column].Add(playerID);
            //update visuals
            //Vector2Int gridPos = new Vector2Int(column, gridData[column].Count - 1);
            //grid[gridPos.x, gridPos.y].UpdateVisuals(playerID);
        }
    }
}
