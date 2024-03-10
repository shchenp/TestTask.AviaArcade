using System;
using Extensions;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : Singleton<ScreenManager>
{
    public Action Start;
    
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private RestartScreen _failedScreen;
    [SerializeField] private RestartScreen _passScreen;
    
    private bool _isFirstAttempt = true;

    protected override void Awake()
    {
        base.Awake();
    
        Instance._startScreen.PlayButtonClicked += StartGame;

        Instance._failedScreen.RestartButtonClicked += RestartGame;

        Instance._passScreen.RestartButtonClicked += RestartGame;
        
        if (Instance._isFirstAttempt)
        {
            ShowStartScreen();
            
            Instance._isFirstAttempt = false;   
        }
        else
        {
            StartGame();
        }
    }

    private void ShowStartScreen()
    {
        Instance._startScreen.gameObject.SetActive(true);
    }

    private void StartGame()
    {
        Instance._startScreen.gameObject.SetActive(false);
        Instance.Start?.Invoke();
    }

    private void RestartGame()
    { 
        SceneManager.LoadScene(GlobalConstants.SCENE_NAME);
        
        Instance._failedScreen.gameObject.SetActive(false);
        Instance._passScreen.gameObject.SetActive(false);
    }

    public void ShowFailedScreen()
    {
        Instance._failedScreen.gameObject.SetActive(true);
    }

    public void ShowPassScreen()
    {
        Instance._passScreen.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Instance._startScreen.PlayButtonClicked -= StartGame;

        Instance._failedScreen.RestartButtonClicked -= RestartGame;

        Instance._passScreen.RestartButtonClicked -= RestartGame;
    }
}
