using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script generates a hexagon script based on parameters given to it
/// </summary>
public class GridGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    public Vector2Int gridSize;
    public Vector2 gridSpacing;
    public float oddColumnOffset;

    [Header("Visuals")]
    [SerializeField] private GameObject hexagonPrefab;

    [Header("Hierarchy Settings")]
    [SerializeField] private Transform gridHolder;


    //=========== Clear Grid ==============
    public void ClearGrid()
    {
        for (int i = gridHolder.childCount - 1; i >= 0; i--)
        {
#if UNITY_EDITOR
            DestroyImmediate(gridHolder.GetChild(i).gameObject);
#else
            Destroy(gridHolder.GetChild(i).gameObject);
#endif
        }
    }

    //=========== Grid Generation ===============
    public GridTile[,] GenerateGrid()
    {
        //clear old grid
        ClearGrid();
        //generate new grid
        GridTile[,] grid = new GridTile[gridSize.x, gridSize.y];
        //generate grid content
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            GenerateGridColumn(i, grid);
        }
        //return generated grid
        return grid;
    }

    //========== Generate Columns ===========
    private void GenerateGridColumn(int columnID, GridTile[,] grid)
    {
        for (int i = 0; i < grid.GetLength(1); i++)
        {
            grid[columnID, i] = GenerateTile(columnID, i);
        }
    }

    //============ Generate Tile ==============
    private GridTile GenerateTile(int xPos, int yPos) //takes position in grid cells
    {
        //gridHolder fallback
        Transform holder = gridHolder != null ? gridHolder : transform;
        //create gridTile object
        GameObject tileObj = Instantiate(hexagonPrefab, holder);
        tileObj.transform.position = GridToWorldPosition(xPos, yPos);
        return tileObj.GetComponent<GridTile>();
    }

    private Vector2 GridToWorldPosition(int xPos, int yPos)
    {
        Vector2 pos = new Vector2(xPos * gridSpacing.x, yPos * gridSpacing.y);
        //if odd column, add offset to y pos
        if (xPos % 2 == 1)  {
            pos.y += oddColumnOffset;
        }
        return pos;
    }
}
