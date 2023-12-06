using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "GridSettings", menuName = "ScriptableObjects/Settings/Grid Settings")]
    public class GridSettingsSO : ScriptableObject
    {
        [Header("Generation Settings")]
        public Vector2Int gridSize;
        public Vector2 gridSpacing;
        public float oddColumnOffset;
        [Space(10f)]
        public float selectorOffset;

        [Header("Visuals")]
        public GameObject hexagonPrefab;
        public GameObject selectorPrefab;
    }
}
