using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    /// <summary>
    /// This script generates a hexagon script based on parameters given to it
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        [Header("Test Settings")]
        public GridSettingsSO testSettings; //used for editor functionality

        [Header("Hierarchy Settings")]
        [SerializeField] private Transform gridHolder;

        private GridSettingsSO gridSettings;

        //=========== Clear Grid ==============
        public void ClearGrid()
        {
            for (int i = gridHolder.childCount - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                //use this version for editor tools
                DestroyImmediate(gridHolder.GetChild(i).gameObject);
#else
                //use this version for build
                Destroy(gridHolder.GetChild(i).gameObject);
#endif
            }
        }

        //=========== Grid Generation ===============
        public List<List<GridTile>> GenerateGrid(GridSettingsSO settings)
        {
            gridSettings = settings;
            //clear old grid
            ClearGrid();
            //initialize / cache vars
            InitializeGridVars();
            //generate new grid
            List<List<GridTile>> grid = new List<List<GridTile>>();
            for (int i = 0; i < gridSettings.gridSize.x; i++)
            {
                grid.Add(GenerateGridColumn(i));
            }
            //return created grid
            return grid;
        }
        private void InitializeGridVars()
        {
            //grid holder fallback
            if (!gridHolder) { gridHolder = transform; }
        }

        //========== Generate Columns ===========
        private List<GridTile> GenerateGridColumn(int columnID)
        {
            //prep data
            List<GridTile> column = new List<GridTile>();
            int distanceFromCenter = CalcDistanceToCenter(columnID);
            int tilesInColumn = gridSettings.gridSize.y - distanceFromCenter;
            //generate column contents
            for (int i = 0; i < tilesInColumn; i++)
            {
                column.Add(GenerateTile(columnID, i, distanceFromCenter));
            }
            return column;
        }

        //calculate how many tiles should be in any given column
        private int CalcDistanceToCenter(int columnID)
        {
            //normalize columnID to always be center or later
            int absColumnID = Mathf.Abs(columnID - ((gridSettings.gridSize.x - 1) / 2));
            //account for increased center size grids
            return Mathf.Max(0, absColumnID - ((gridSettings.centerSize - 1) / 2));
        }

        //============ Generate Tile ==============
        private GridTile GenerateTile(int xPos, int yPos, int distanceFromCenter)
        {
#if UNITY_EDITOR
            //use this version in editor for convenience
            GameObject tileObj = UnityEditor.PrefabUtility.InstantiatePrefab(gridSettings.hexagonPrefab, gridHolder) as GameObject;
#else
            //use this version for build
            GameObject tileObj = Instantiate(gridSettings.hexagonPrefab, gridHolder);
#endif
            bool isOffsetCenter = IsOffsetCenter(xPos, distanceFromCenter);
            tileObj.transform.position = GridToWorldPos(xPos, yPos, distanceFromCenter, isOffsetCenter);
            //setup tile data
            GridTile tile = tileObj.GetComponent<GridTile>();
            tile.gridPos = new Vector2Int(xPos, yPos);
            tile.isCenterTile = distanceFromCenter == 0;
            tile.isRaisedCenterTile = isOffsetCenter;
            return tile;
        }

        private Vector2 GridToWorldPos(int xPos, int yPos, int distanceFromCenter, bool isOffsetCenter)
        {
            //calculate y offset
            float yOffset = distanceFromCenter * gridSettings.yColumnOffset;
            if (isOffsetCenter) { yOffset += gridSettings.yColumnOffset; }
            //convert grid position to world position
            return new Vector2(xPos * gridSettings.gridSpacing.x, yPos * gridSettings.gridSpacing.y + yOffset);
        }

        private bool IsOffsetCenter(int xPos, int distanceFromCenter)
        {
            //is in center check
            if (distanceFromCenter == 0) {
                //get position within center
                int centerPos = xPos - ((gridSettings.gridSize.x - gridSettings.centerSize) / 2);
                return centerPos % 2 == 1;
            }
            return false;
        }

        //=============== Grid Center ================
        public Vector2 GetGridCenter()
        {
            return GetGridBounds() / 2f - (gridSettings.gridSpacing / 2f);
        }
        //=============== Grid Bounds ==================
        public Vector2 GetGridBounds()
        {
            return gridSettings.gridSize * gridSettings.gridSpacing;
        }
    }
}