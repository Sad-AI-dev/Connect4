using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    /// <summary>
    /// Holds data for the visuals of each tile
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridTile : MonoBehaviour
    {
        [Header("External Components")]
        public SpriteRenderer spriteRenderer;

        //positional vars
        [HideInInspector] public bool isRaisedCenterTile; //used by gridDirectionUtil

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
    }
}
