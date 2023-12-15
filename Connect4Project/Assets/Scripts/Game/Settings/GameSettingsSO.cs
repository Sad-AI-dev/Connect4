using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Settings/Game Settings")]
    public class GameSettingsSO : ScriptableObject
    {
        public GameRulesSO rules;
        public GridSettingsSO gridSettings;
        public PlayerColorSettingsSO colorSettings;
    }
}
