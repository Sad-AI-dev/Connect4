using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class GameStateManager : MonoBehaviour
    {
        [Header("GameSettings")]
        [SerializeField] private GameSettingsSO settings;

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

        private void Start()
        {
            //notify game has started
            EventBus<GameStartEvent>.Invoke(new GameStartEvent { settings = settings });
            //notify next turn
            EventBus<NextTurnEvent>.Invoke(new NextTurnEvent { currentPlayerID = currentPlayer });
        }

        //========= Handle Try Place ==============
        private void HandleTryPlace(TryPlaceEvent eventData)
        {
            if (gridManager.CanPlace(eventData.targetColumn))
            {
                gridManager.PlaceTile(currentPlayer, eventData.targetColumn, eventData.direction);
                //victory check
                if (gridManager.FindLongestSequence(currentPlayer) >= settings.rules.sequenceToWin) {
                    //notify game end
                    EventBus<GameEndEvent>.Invoke(new GameEndEvent { winnerID = currentPlayer });
                }
                else {
                    currentPlayer++;
                    //loop check
                    if (currentPlayer >= settings.rules.playerCount) { currentPlayer = 0; }
                    //notify next turn
                    EventBus<NextTurnEvent>.Invoke(new NextTurnEvent { currentPlayerID = currentPlayer });
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
