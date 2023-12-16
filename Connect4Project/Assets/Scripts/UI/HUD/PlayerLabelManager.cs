using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerLabelManager : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform holder;

        //vars
        private PlayerCard[] cards;

        private void Awake()
        {
            //initialize vars
            if (!holder) { holder = transform; }
            //listen for game start
            EventBus<GameStartEvent>.AddListener(Initialize);
            EventBus<NextTurnEvent>.AddListener(HandleNextTurn);
        }

        //========== Handle Game Start ===========
        private void Initialize(GameStartEvent eventData)
        {
            cards = new PlayerCard[eventData.settings.rules.playerCount];
            //create player cards
            for (int i = 0; i < cards.Length; i++) {
                cards[i] = CreatePlayerCard(i, eventData.settings.colorSettings);
            }
        }
        private PlayerCard CreatePlayerCard(int playerID, PlayerColorSettingsSO colorSettings)
        {
            PlayerCard card = Instantiate(prefab, holder).GetComponent<PlayerCard>();
            card.label.text = "P" + (playerID + 1); //playerID starts at 0
            card.bgImage.color = colorSettings.GetPlayerColor(playerID);
            return card;
        }

        //========== Handle Next Turn ===========
        private void HandleNextTurn(NextTurnEvent eventData)
        {
            //reset last card
            if (eventData.currentPlayerID == 0) { cards[^1].ResetPosition(); }
            else { cards[eventData.currentPlayerID - 1].ResetPosition(); }
            //highlight current player card
            cards[eventData.currentPlayerID].Highlight();
        }

        //========= Handle Destroy ==========
        private void OnDestroy()
        {
            EventBus<GameStartEvent>.RemoveListener(Initialize);
            EventBus<NextTurnEvent>.RemoveListener(HandleNextTurn);
        }
    }
}
