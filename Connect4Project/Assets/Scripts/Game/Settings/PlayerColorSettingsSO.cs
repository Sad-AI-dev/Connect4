using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "PlayerColorSettings", menuName = "ScriptableObjects/Settings/Player Color Settings")]
    public class PlayerColorSettingsSO : ScriptableObject
    {
        [SerializeField] private Color defaultColor;
        [SerializeField] private List<Color> playerColors;

        public Color GetPlayerColor(int playerID)
        {
            //bounds check
            if (playerID >= 0 && playerID < playerColors.Count) {
                return playerColors[playerID];
            }
            else { return defaultColor; } //fallback
        }

        public string GetPlayerHexCode(int playerID)
        {
            Color playerColor = GetPlayerColor(playerID);
            return "#" + ColorUtility.ToHtmlStringRGB(playerColor);
        }
    }
}
