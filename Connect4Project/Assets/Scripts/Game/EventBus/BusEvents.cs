using UnityEngine;

namespace Game {
    /// <summary>
    /// Base Bus Event class, inherit from this to create a new bus event
    /// </summary>
    public class BusEvent { }

    //========= State Events =========
    public class GameStartEvent : BusEvent {
        public GameSettingsSO settings;
    }

    public class NextTurnEvent : BusEvent {
        public int currentPlayerID;
    }

    public class GameEndEvent : BusEvent {
        public int winnerID;
    }

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
