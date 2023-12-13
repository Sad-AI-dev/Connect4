using UnityEngine;

namespace Game {
    /// <summary>
    /// Base Bus Event class, inherit from this to create a new bus event
    /// </summary>
    public class BusEvent { }

    //========= Camera Events ===========
    public class CameraFrameReqEvent : BusEvent {
        public Vector2 center;
        public Vector2 bounds;
    }

    //======= Game Events =========
    public class HoverTileEvent : BusEvent {
        public GridTile hoveredTile;
    }

    public class TryPlaceEvent : BusEvent {
        public int targetColumn;
        public GridDirection direction;
    }
}
