using UnityEngine;

namespace Game {
    /// <summary>
    /// Base Bus Event class, inherit from this to create a new bus event
    /// </summary>
    public class BusEvent { }

    //========= Camera Events ===========
    public class CameraFrameReqEvent : BusEvent {
        public bool teleport = false;
        public Vector2 center;
        public Vector2 bounds;
    }

    //======= Game Events =========
    public class TryPlaceEvent : BusEvent {
        public int targetColumn;
    }
}
