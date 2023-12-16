using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game {
    public class ResultScreenController : MonoBehaviour
    {
        [Header("Win Settings")]
        [SerializeField] private string winStartString = "Player";
        [SerializeField] private string winEndString = "wins!";

        [Header("Draw Settings")]
        [SerializeField] private string drawString = "Draw!";

        [Header("Refs")]
        [SerializeField] private TMP_Text title;

        private void Awake()
        {
            EventBus<GameEndEvent>.AddListener(HandleGameEnd);
            //hide by default
            gameObject.SetActive(false);
        }

        //========= Handle Game Win =========
        private void HandleGameEnd(GameEndEvent eventData)
        {
            if (!eventData.isDraw) {
                SetupWinTitle(eventData.winnerID, eventData.settings.colorSettings);
            }
            else {
                title.text = drawString;
            }
            //show screen
            gameObject.SetActive(true);
        }

        private void SetupWinTitle(int winnerID, PlayerColorSettingsSO settings)
        {
            title.text = $"{winStartString} <color={settings.GetPlayerHexCode(winnerID)}>{winnerID + 1}</color> {winEndString}";
        }

        //========= Handle Destroy =========
        private void OnDestroy()
        {
            EventBus<GameEndEvent>.RemoveListener(HandleGameEnd);
        }
    }
}
