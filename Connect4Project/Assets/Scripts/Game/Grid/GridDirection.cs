using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// This enum is reused in various places throughout the project. It represents a direction on the grid
    /// </summary>
    public enum GridDirection { Up, Down, LeftUp, LeftDown, RightUp, RightDown }

    public static class GridDirectionUtil
    {
        public static Vector2Int GridDirectionToVector2(GridDirection gridDir, GridTile currentTile, GridManager grid)
        {
            //generic conversion from readable direction to vector2 direction
            return gridDir switch
            {
                GridDirection.Up => Vector2Int.up,
                GridDirection.Down => Vector2Int.down,
                GridDirection.LeftUp => new Vector2Int(-1, GetDirYOffset(gridDir, currentTile, grid)),
                GridDirection.LeftDown => new Vector2Int(-1, GetDirYOffset(gridDir, currentTile, grid)),
                GridDirection.RightUp => new Vector2Int(1, GetDirYOffset(gridDir, currentTile, grid)),
                GridDirection.RightDown => new Vector2Int(1, GetDirYOffset(gridDir, currentTile, grid)),
                _ => Vector2Int.zero //this should never get called
            };
        }

        //====== Conversion helpers ======
        private static int GetDirYOffset(GridDirection dir, GridTile currentTile, GridManager grid)
        {
            bool isLeft = dir == GridDirection.LeftUp || dir == GridDirection.LeftDown;
            bool isDown = dir == GridDirection.LeftDown || dir == GridDirection.RightDown;
            //center case
            if (currentTile.isCenterTile) {
                return (currentTile.isRaisedCenterTile ? 1 : 0) + (isDown ? -1 : 0);
            }
            //non center case
            else {
                bool isLeftSide = currentTile.gridPos.x < (grid.gridVisuals.Count - 1) / 2f;
                if (!isLeft) { isLeftSide = !isLeftSide; } //flip result if dir is right
                //return result
                return (isLeftSide ? 0 : 1) + (isDown ? -1 : 0);
            }
        }
    }
}
