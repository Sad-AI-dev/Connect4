using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
    public class SceneLoader : MonoBehaviour
    {
        //=========== Load Scene =============
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        //=========== Load Scene Relative =============
        public void LoadSceneRelative(int relativeIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + relativeIndex);
        }

        //=========== Quit Game ===========
        public void Quit()
        {
            Application.Quit();
        }
    }
}
