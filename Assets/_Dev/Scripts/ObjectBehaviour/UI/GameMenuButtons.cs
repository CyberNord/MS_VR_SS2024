using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Dev.Scripts.ObjectBehaviour.UI
{
    public class GameMenuButtons : MonoBehaviour
    {
        public void Exit()
        {
            Application.Quit();
        }
        
        public void LoadScene()
        {
            
            switch (Constants.SelectedScene)
            {
                case Constants.GameState.StartMenu:
                    SceneManager.LoadScene("StartMenu");
                    break;
                case Constants.GameState.M1:
                    SceneManager.LoadScene("Scene1");
                    break;
                case Constants.GameState.M2:
                    SceneManager.LoadScene("Scene2");
                    break;
                case Constants.GameState.M3:
                    SceneManager.LoadScene("Scene3");
                    break;
                default:
                    Debug.LogWarning("Error in loading " + Constants.SelectedScene);
                    throw new ArgumentOutOfRangeException(nameof(Constants.SelectedScene), Constants.SelectedScene, null);
            }
        }
    }
}
