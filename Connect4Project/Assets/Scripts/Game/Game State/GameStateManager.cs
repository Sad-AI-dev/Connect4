using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class GameStateManager : MonoBehaviour
    {
        [Header("GameSettings")]
        [SerializeField] private GameRulesSO rules;

        [Header("References")]
        [SerializeField] private GridManager gridManager;

        //game state vars
        private int currentPlayer;

        private void Awake()
        {
            //subscribe to event bus
            EventBus<TryPlaceEvent>.AddListener(HandleTryPlace);
            //initializeVars
            currentPlayer = 0;
        }

        //========= Handle Try Place ==============
        private void HandleTryPlace(TryPlaceEvent eventData)
        {
            if (gridManager.CanPlace(eventData.targetColumn))
            {
                gridManager.PlaceTile(currentPlayer, eventData.targetColumn, eventData.direction);
                //victory check
                if (gridManager.FindLongestSequence(currentPlayer) >= rules.sequenceToWin) {
                    Debug.Log($"{currentPlayer} won!");
                }
                else {
                    currentPlayer++;
                    //loop check
                    if (currentPlayer >= rules.playerCount) { currentPlayer = 0; }
                }
            }
        }

        //======== Handle Destroy ========
        private void OnDestroy()
        {
            //unsubscribe from event bus
            EventBus<TryPlaceEvent>.RemoveListener(HandleTryPlace);
        }
    }
}
