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
        [SerializeField] private SelectorGenerator selectorGenerator;

        //vars
        private List<List<GridTile>> gridVisuals;
        private List<List<int>> gridData;

        private void Start()
        {
            //create grid
            gridVisuals = gridGenerator.GenerateGrid();
            InitializeGridData();
            //generate selectors, if selector generator is set
            if (selectorGenerator) { selectorGenerator.GenerateSelectors(gridVisuals); }
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
    }
}
