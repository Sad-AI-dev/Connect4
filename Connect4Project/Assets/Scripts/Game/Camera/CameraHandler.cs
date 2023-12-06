using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class CameraHandler : MonoBehaviour
    {
        [Header("Framing Settings")]
        [SerializeField] private Vector2 defaultTeleportOffset;

        //vars
        private Camera cam;

        //movement vars
        private Coroutine moveRoutine;
        private Vector3 moveDestination;

        private void Awake()
        {
            cam = Camera.main;

            //listen to bus events
            EventBus<CameraMoveReqEvent>.AddListener(HandleMoveReqEvent);
        }

        //========= Handle Move Req Events ========
        private void HandleMoveReqEvent(CameraMoveReqEvent eventData)
        {
            if (eventData.teleport) { Teleport(eventData.targetPosition + defaultTeleportOffset); }
            else { StartMove(eventData.targetPosition); }
        }

        //================================ Move Camera ==============================
        //======== Teleport ========
        private void Teleport(Vector2 destination)
        {
            cam.transform.position = new Vector3(destination.x, destination.y, cam.transform.position.z); //maintain z pos
        }

        //========= Move ===========
        private void StartMove(Vector2 destination)
        {

        }

        //========= Handle Destroy ==========
        private void OnDestroy()
        {
            //unsubscribe from bus events
            EventBus<CameraMoveReqEvent>.RemoveListener(HandleMoveReqEvent);
        }
    }
}
