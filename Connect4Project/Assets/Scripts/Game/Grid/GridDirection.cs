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
        public static Vector2Int GridDirectionToVector2(GridDirection gridDir, GridTile currentTile)
        {
            //handle offset center tiles exception
            Vector2Int offset = currentTile.isRaisedCenterTile ? Vector2Int.up : Vector2Int.zero;
            //generic conversion from readable direction to vector2 direction
            return gridDir switch
            {
                GridDirection.Up => Vector2Int.up,
                GridDirection.Down => Vector2Int.down,
                GridDirection.LeftUp => Vector2Int.left + offset,
                GridDirection.LeftDown => new Vector2Int(-1, -1) + offset,
                GridDirection.RightUp => Vector2Int.right + offset,
                GridDirection.RightDown => new Vector2Int(1, -1) + offset,
                _ => Vector2Int.zero //this should never get called
            };
        }
    }
}
