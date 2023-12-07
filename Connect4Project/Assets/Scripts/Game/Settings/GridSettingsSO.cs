using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "GridSettings", menuName = "ScriptableObjects/Settings/Grid Settings")]
    public class GridSettingsSO : ScriptableObject
    {
        [Header("Generation Settings")]
        public Vector2Int gridSize;
        public int centerSize;
        [Space(10f)]
        public Vector2 gridSpacing;
        public float yColumnOffset;

        [Header("Visuals")]
        public GameObject hexagonPrefab;
        public GameObject selectorPrefab;

        //============ Editor Polish ============
        private void OnValidate()
        {
            //gridSize check
            if (gridSize.x % 2 == 0) { gridSize.x++; } //force gridsize.x to be uneven
            //center size check
            if (centerSize % 2 == 0) { centerSize++; } //force centerSize to be uneven
        }
    }
}
