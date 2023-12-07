using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class SelectorGenerator : MonoBehaviour
    {
        //[Header("Generation Settings")]
        //[SerializeField] private GridSettingsSO gridSettings;

        //[Header("Hierarchy Settings")]
        //[SerializeField] private Transform selectorHolder;

        //private void Start()
        //{
        //    GenerateSelectors();
        //}

        ////=========== Generate Selectors ===========
        //private void GenerateSelectors()
        //{
        //    //cache data
        //    Transform holder = selectorHolder != null ? selectorHolder : transform; //selectorHolder fallback
        //    Vector2[] selectorPositions = GetSelectorPositions();
        //    //create selector
        //    for (int i = 0; i < selectorPositions.Length; i++)
        //    {
        //        GenerateSelector(selectorPositions[i], holder, i);
        //    }
        //}

        //private Vector2[] GetSelectorPositions()
        //{
        //    Vector2[] positions = new Vector2[gridSettings.gridSize.x];
        //    float height = gridSettings.gridSize.y * gridSettings.gridSpacing.y + gridSettings.selectorOffset;

        //    for (int i = 0; i < positions.Length; i++)
        //    {
        //        positions[i] = new Vector2(i * gridSettings.gridSpacing.x, height);
        //    }
        //    return positions;
        //}

        ////========== Create Objects ===========
        //private void GenerateSelector(Vector2 pos, Transform holder, int column)
        //{
        //    GameObject obj = Instantiate(gridSettings.selectorPrefab, holder);
        //    obj.transform.position = pos;
        //    obj.GetComponent<Selector>().columnID = column;
        //}
    }
}
