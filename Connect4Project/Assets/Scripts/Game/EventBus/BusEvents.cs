using UnityEngine;

namespace Game {
    /// <summary>
    /// Base Bus Event class, inherit from this to create a new bus event
    /// </summary>
    public class BusEvent { }

    //========= Camera Events ===========
    public class CameraMoveReqEvent : BusEvent {
        public bool teleport = false;
        public Vector2 targetPosition;
    }

    //======= Game Events =========
    public class TryPlaceEvent : BusEvent {
        public int targetColumn;
    }
}
