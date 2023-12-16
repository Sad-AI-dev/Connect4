using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game;

namespace UI
{
    public class SettingsScreenManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameSettingsSO settingsToModify;

        [Header("Game Rule Refs")]
        [SerializeField] private TMP_InputField playerCountField;
        [SerializeField] private TMP_InputField toConnectField;
        [SerializeField] private int minToConnect = 3; //less than 3 does not make sense from a gameplay perspective

        [Header("Grid Settings Refs")]
        [SerializeField] private TMP_InputField gridWidthField;
        [SerializeField] private TMP_InputField gridHeightField;
        [SerializeField] private TMP_InputField gridCenterField;

        private void Awake()
        {
            Initialize();
        }

        //=========== Initialize ============
        private void Initialize()
        {
            //initialize game rule fiels
            if (playerCountField) { 
                playerCountField.text = settingsToModify.rules.playerCount.ToString();
                playerCountField.onEndEdit.AddListener(SetPlayerCount); //setup listener
            }
            if (toConnectField) { 
                toConnectField.text = settingsToModify.rules.sequenceToWin.ToString();
                toConnectField.onEndEdit.AddListener(SetToConnect); //setup listener
            }
            //initialize grid settings fields
            if (gridWidthField) { 
                gridWidthField.text = settingsToModify.gridSettings.gridSize.x.ToString();
                gridWidthField.onEndEdit.AddListener(SetGridWidth); //setup listener
            }
            if (gridHeightField) { 
                gridHeightField.text = settingsToModify.gridSettings.gridSize.y.ToString();
                gridHeightField.onEndEdit.AddListener(SetGridHeight); //setup listener
            }
            if (gridCenterField) { 
                gridCenterField.text = settingsToModify.gridSettings.centerSize.ToString();
                gridCenterField.onEndEdit.AddListener(SetGridCenter); //setup listener
            }
        }

        //============ Process Settings =================
        //======= Game Rules ========
        public void SetPlayerCount(string input)
        {
            if (int.TryParse(input, out int playerCount)) {
                if (playerCount < 2) { 
                    playerCount = 2; //must have at least 2 players
                    playerCountField.text = playerCount.ToString();
                }
                settingsToModify.rules.playerCount = playerCount;
            }
        }

        public void SetToConnect(string input)
        {
            if (int.TryParse(input, out int toConnect)) {
                //min to connect check
                if (toConnect < minToConnect) {
                    toConnect = minToConnect;
                    toConnectField.text = toConnect.ToString();
                }
                settingsToModify.rules.sequenceToWin = toConnect;
            }
        }

        //========= Grid Settings ===========
        public void SetGridWidth(string input)
        {
            if (int.TryParse(input, out int width)) {
                if (width < 1) { width = 0; } //must be at least 1, set to 0 so next block registers it
                if (width % 2 == 0) { //width cannot be even
                    width++;
                    gridWidthField.text = width.ToString();
                }
                settingsToModify.gridSettings.gridSize.x = width;
            }
        }

        public void SetGridHeight(string input)
        {
            if (int.TryParse(input, out int height)) {
                if (height < 1) {
                    height = 1; //height must be at least 1
                    gridHeightField.text = height.ToString();
                }
                settingsToModify.gridSettings.gridSize.y = height;
            }
        }

        public void SetGridCenter(string input)
        {
            if (int.TryParse(input, out int centerSize)) {
                if (centerSize < 1) { centerSize = 0; } //must be at least 1, set to 0 so next block registers it
                if (centerSize % 2 == 0) {
                    centerSize++;
                    gridCenterField.text = centerSize.ToString();
                }
                settingsToModify.gridSettings.centerSize = centerSize;
            }
        }
    }
}
