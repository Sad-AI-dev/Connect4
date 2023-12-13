using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class SelectorManager : MonoBehaviour
    {
        //these are all children of the manager and are set through the unity editor
        [Header("Selectors")]
        [SerializeField] private Selector rightDownSelector;
        [SerializeField] private Selector downSelector;
        [SerializeField] private Selector leftDownSelector;

        [Header("Refs")]
        [SerializeField] private GridManager gridManager;

        private Selector[] selectors; //convenient shortcut for actions that need to be shared across all selectors

        private void Awake()
        {
            //compile selectors
            selectors = new Selector[] { rightDownSelector, downSelector, leftDownSelector };
            //hide selectors by default
            ResetSelectors();
            //listen to events
            EventBus<TryPlaceEvent>.AddListener(HandlePlaceTile);
            EventBus<HoverTileEvent>.AddListener(HandleHoverTile);
        }

        //========== Handle Selector Click =========
        private void HandlePlaceTile(TryPlaceEvent eventData)
        {
            ResetSelectors();
        }

        //=========== Move Selectors ==========
        private void HandleHoverTile(HoverTileEvent eventData)
        {
            MoveSelectors(eventData.hoveredTile);
            SetSelectorData(eventData.hoveredTile);
            ShowSelectors(eventData.hoveredTile);
        }

        //======= Move Selectors =======
        private void MoveSelectors(GridTile hoveredTile)
        {
            Vector3 targetPos = gridManager.GetTopTileInColumn(hoveredTile.gridPos.x).transform.position;
            transform.position = targetPos;
        }

        //======= Selector Data =========
        private void SetSelectorData(GridTile hoveredTile)
        {
            Array.ForEach(selectors, (Selector selector) => selector.columnID = hoveredTile.gridPos.x);
        }

        //========= Show Selectors ============
        private void ShowSelectors(GridTile hoveredTile)
        {
            ResetSelectors();
            ShowValidSelectors(hoveredTile);
        }

        private void ResetSelectors()
        {
            Array.ForEach(selectors, (Selector selector) => selector.gameObject.SetActive(false));
        }

        private void ShowValidSelectors(GridTile hoveredTile)
        {
            downSelector.gameObject.SetActive(true); //down selector is always valid
            rightDownSelector.gameObject.SetActive(RightDownIsValid(hoveredTile));
            leftDownSelector.gameObject.SetActive(LeftDownIsValid(hoveredTile));
        }

        //=== Show Selectors Conditionals ===
        private bool RightDownIsValid(GridTile hoveredTile)
        {
            return hoveredTile.isRaisedCenterTile || //raised center exception
                 hoveredTile.isCenterTile && IsEdgeCenterTile(hoveredTile.gridPos.x, -1) || //center edge check
                 !hoveredTile.isCenterTile && hoveredTile.gridPos.x < (gridManager.gridVisuals.Count - 1) / 2f; //non center left side check
        }

        private bool LeftDownIsValid(GridTile hoveredTile)
        {
            return hoveredTile.isRaisedCenterTile || //raised center exception
                hoveredTile.isCenterTile && IsEdgeCenterTile(hoveredTile.gridPos.x, 1) || //center edge check
                !hoveredTile.isCenterTile && hoveredTile.gridPos.x > (gridManager.gridVisuals.Count - 1) / 2f; //non center right side check
        }

        private bool IsEdgeCenterTile(int column, int checkOffset)
        {
            return column > 0 && column < gridManager.gridVisuals.Count - 1 && //bounds check
                gridManager.gridVisuals[column + checkOffset].Count > 0 && //check if column to check is not empty
                !gridManager.gridVisuals[column + checkOffset][0].isCenterTile; //check result
        }

        //=========== Handle Destroy ==========
        private void OnDestroy()
        {
            EventBus<TryPlaceEvent>.RemoveListener(HandlePlaceTile);
            EventBus<HoverTileEvent>.RemoveListener(HandleHoverTile);
        }
    }
}
