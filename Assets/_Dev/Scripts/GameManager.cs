using System;
using _Dev.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public Constants.GameState gameState = Constants.GameState.StartMenu;
    
    // Make sure it's the one and only
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadScene()
    {
        switch (gameState)
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
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }
}
