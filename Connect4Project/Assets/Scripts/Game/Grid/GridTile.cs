using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridTile : MonoBehaviour
    {
        public int ownerID = -1;

        [Header("External Components")]
        public SpriteRenderer spriteRenderer;

        private void Awake()
        {
            //guarentee valid sprite renderer ref
            if (!spriteRenderer) { spriteRenderer = GetComponent<SpriteRenderer>(); }
        }

        //========= Update Visuals ==========
        public void UpdateVisuals(int playerID)
        {
            ownerID = playerID;
            gameObject.SetActive(false);
        }
    }
}
