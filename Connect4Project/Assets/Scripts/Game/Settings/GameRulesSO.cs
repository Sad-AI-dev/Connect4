using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "GameRules", menuName = "ScriptableObjects/Settings/Game Rules")]
    public class GameRulesSO : ScriptableObject
    {
        public int playerCount = 2;
        public int sequenceToWin = 4;
    }
}
