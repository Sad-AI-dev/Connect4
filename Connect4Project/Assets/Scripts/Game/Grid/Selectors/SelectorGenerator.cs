using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class SelectorGenerator : MonoBehaviour
    {
        [Header("Generation Settings")]
        [SerializeField] private GameObject selectorPrefab;
        [Space(10f)]
        [SerializeField] private float selectorYOffset;

        [Header("Hierarchy Settings")]
        [SerializeField] private Transform selectorHolder;

        //=========== Generate Selectors ===========
        public void GenerateSelectors(List<List<GridTile>> grid)
        {
            //selectorHolder fallback
            if (!selectorHolder) { selectorHolder = transform; }
            //create downward selector objects
            CreateDownSelectors(grid);
        }
        //======= Selector Passes ========
        private void CreateDownSelectors(List<List<GridTile>> grid)
        {
            for (int i = 0; i < grid.Count; i++) {
                //calculate position
                Vector3 selectorPos = grid[i][^1].transform.position + new Vector3(0, selectorYOffset, 0);
                //create object
                CreateSelector(selectorPos, selectorHolder, i, GridDirection.Down);
            }
        }

        //========== Create Objects ===========
        private void CreateSelector(Vector2 pos, Transform holder, int column, GridDirection placeDirection)
        {
            GameObject obj = Instantiate(selectorPrefab, holder);
            obj.transform.position = pos;
            //set selector data
            Selector selector = obj.GetComponent<Selector>();
            selector.columnID = column;
            selector.placeDirection = placeDirection;
        }

    }
}
