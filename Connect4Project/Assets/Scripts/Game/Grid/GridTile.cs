using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game {
    /// <summary>
    /// Holds data for the visuals of each tile
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridTile : MonoBehaviour, IPointerEnterHandler
    {
        [Header("External Components")]
        public SpriteRenderer spriteRenderer;

        //positional vars
        [HideInInspector] public Vector2Int gridPos;
        [HideInInspector] public bool isCenterTile; //used by selector
        [HideInInspector] public bool isRaisedCenterTile; //used by gridDirectionUtil and selector

        private void Awake()
        {
            //guarentee valid sprite renderer ref
            if (!spriteRenderer) { spriteRenderer = GetComponent<SpriteRenderer>(); }
        }

        //========= Update Visuals ==========
        public void UpdateVisuals(int playerID)
        {
            gameObject.SetActive(false);
        }

        //========== Pointer Event Handling ==========
        public void OnPointerEnter(PointerEventData pointerData)
        {
            EventBus<HoverTileEvent>.Invoke(new HoverTileEvent { hoveredTile = this });
        }
    }
}
