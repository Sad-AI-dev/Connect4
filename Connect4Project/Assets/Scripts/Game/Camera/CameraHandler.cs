using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class CameraHandler : MonoBehaviour
    {
        [Header("Framing Settings")]
        [SerializeField] private Vector2 defaultTeleportOffset;
        [SerializeField] private float defaultDeadSpace;

        //vars
        private Camera cam;

        //movement vars
        private Coroutine moveRoutine;
        private Vector3 moveDestination;

        private void Awake()
        {
            cam = Camera.main;

            //listen to bus events
            EventBus<CameraFrameReqEvent>.AddListener(HandleMoveReqEvent);
        }

        //========= Handle Move Req Events ========
        private void HandleMoveReqEvent(CameraFrameReqEvent eventData)
        {
            if (eventData.teleport) { 
                //move camera
                Teleport(eventData.center + defaultTeleportOffset);
                //set camera size
                cam.orthographicSize = GetTargetSize(eventData.bounds);
            }
            else { StartMove(eventData.center); }
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

        //=============================== Frame Bounds ===================================
        //======= GetTargetSize =======
        private float GetTargetSize(Vector2 bounds)
        {
            float targetAspect = bounds.x / bounds.y;
            if (cam.aspect >= targetAspect) {
                //scale based on y bounds
                return bounds.y / 2f + defaultDeadSpace;
            }
            else {
                //scale based on x bounds
                float difference = targetAspect / cam.aspect;
                return (bounds.y / 2) * difference + defaultDeadSpace;
            }
        }

        //========= Handle Destroy ==========
        private void OnDestroy()
        {
            //unsubscribe from bus events
            EventBus<CameraFrameReqEvent>.RemoveListener(HandleMoveReqEvent);
        }
    }
}
